<template>
  <div
    class="chat-card"
    style=""
  >
    <div class="w-100">
      <div class="chat-header w-100">
        <div class="chat-header--profile">
          <span class="chat-header--profile-text">{{receiverUser}}</span>
        </div>
        <div class="chat-header--close-button">
          <span class="text-warning chat-header--close-button-text" @click="closeChat">x</span>
        </div>
      </div>
      <div class="chat-messages">
        <div class="chat-messages-placer">
          <div
            v-for="(message, index) in messages"
            :key="message.id"
          >
            <p v-if="!messages[index + 1] || messages[index + 1] && message.time !== messages[index + 1].time" class="chat-messages-time">{{ message.time }}</p>
            <div
              v-if="message.sender"
              class="chat-messages--message chat-messages--message_sent"
              style="float: right"
            >
              <span><b>{{ message.user }}:</b>
                {{ message.content }}</span>
            </div>
            <div
              class="chat-messages--message chat-messages--message_received"
              v-else
            >
              <span class="chat-messages--message_received"><b>{{ message.user }}:</b>
                {{ message.content }}</span>
            </div>
          </div>
        </div>
        <!-- style="top: 210px; left: 10px;" -->
      </div>
      <div class="chat-bottom">
        <input
          v-model="inputText"
          class="chat-bottom--input"
          style="width: 15rem;"
          type="text"
          v-on:keyup.enter="sendMessage"
        />
        <div
          class="chat-bottom--send-buttom"
          @click="sendMessage"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            class="bi bi-send"
            viewBox="0 0 16 16"
          >
            <path d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576 6.636 10.07Zm6.787-8.201L1.591 6.602l4.339 2.76 7.494-7.493Z" />
          </svg>
        </div>
      </div>
    </div>
  </div>
</template>

<style>
.chat-card {
  width: 18rem;
  height: 20rem;
  border: 1px solid black;
  border-radius: 5px;
  display: inline-block;
  margin: 2rem;
  background-color: white;
}

.chat-header {
  background-color: #00295e;
  height: 1.6rem;
}

.chat-header--profile {
  display: inline-block;
  width: 92%;
}

.chat-header--close-button {
  display: inline-block;
  width: 7%;
}

.chat-header--profile-text {
  color: white;
  font-weight: bold;
  padding-left: 15px;
}

.chat-header--close-button-text {
  font-weight: bold;
  cursor: pointer;
}

.chat-messages {
  height: 250px;
}

.chat-messages-placer {
  display: flex;
  height: 240px;
  flex-direction: column-reverse;
  padding: 0 10px;
  overflow: auto;
}

.chat-messages-placer::-webkit-scrollbar {
  width: 5px;
}


.chat-messages-placer::-webkit-scrollbar-thumb {
  background-color: rgb(255,193,7);
  border-radius: 20px;
}

.chat-messages-time {
  font-size: 12px;
  text-align: center;
  color: grey;
  margin: 5px 0;
}

.chat-messages--message {
  margin: 0 0 5px 0;
  padding: 0 5px;
  max-width: 15rem;
  display: inline-block;
  border-radius: 5px;
}

.chat-messages--message_sent {
  background-color: #c0c0c0;
}

.chat-messages--message_received {
  background-color: #98c1f8;
}

.chat-bottom {
  margin: 6px;
  border: 2px solid black;
  border-radius: 5px;
  display: inline-block;
}

.chat-bottom--send-buttom {
  display: inline;
  padding: 5px;
  border-left: 2px solid black;
}

.chat-bottom--input {
  border: none;
}
</style>

<script>
import { onBeforeUnmount, ref } from 'vue'
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr'

export default {
  props: {
    loggedUser: {
      type: String,
      default: 'abc'
    },
    receiverUser: {
      type: String
    }
  },
  emits: ['closeChat'],
  setup (props, {emit}) {
    const sendMessage = () => {
      if (inputText.value) {
        const date = new Date()
        const formattedDate = `${date.getHours()}:${date.getMinutes()}`
        messages.value.unshift({
          id: 123,
          user: 'Você',
          content: inputText.value,
          sender: true,
          time: formattedDate,
        })
        connection.invoke('SendMessage', props.loggedUser, inputText.value, props.receiverUser, formattedDate)
        inputText.value = ''
      }
    }

    const receiveMessage = (user, message, receiver, time) => {
      if (user == props.receiverUser && receiver == props.loggedUser) {
        messages.value.unshift({
          id: 123,
          user: user,
          content: message,
          time: time
        })
      }
    }

    const connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl("https://localhost:7173/chat", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();

    let startedPromise = null
    function start () {
      startedPromise = connection.start().then()
        .catch(err => {
          console.error('Failed to connect with hub', err)
          return new Promise((resolve, reject) =>
            setTimeout(() => start().then(resolve).catch(reject), 5000))
        })
      return startedPromise
    }

    connection.on('ReceiveMessage', (user, message, receiver, time) => {
      receiveMessage(user, message, receiver, time)
    })
    start()

    onBeforeUnmount(() => {
      connection.stop().then(() => {
        setTimeout(() => {
        }, 2000)
      })
    })

    const closeChat = () => {
      emit('closeChat')
    }

    const messages = ref([])

    const inputText = ref('')

    return {
      messages,
      inputText,
      sendMessage,
      closeChat
    }
  }
}
</script>
