<template>
  <div class="chat__messages__container">
    <div v-if="!chatInfo" class="chat__empty__container">
      <div>
        <img
          class="chat__empty__container__image"
          src="https://i.imgur.com/zBJnlSa.png"
        />
        <p>Select a conversation</p>
        <p>or</p>
        <button
          class="chat__empty__container__button"
          @click="emit('inviteUser')"
        >
          Start a new
        </button>
      </div>
    </div>
    <div v-else class="chat__messages__container__chat">
      <div class="chat__messages__header">
        <div class="chat__messages__name">{{ chatInfo.title }}</div>
        <div class="chat__messages__info">Available</div>
      </div>
      <div class="chat__messages__area">
        <div class="chat__messages__wrapper" v-for="(message, index) in chatInfo.messages" :key="message.id">
          <div
            v-if="message.action == 1"
            class="chat__message--text"
            :class="message.ownMessage
                ? 'chat__message--sent'
                : 'chat__message--received'
            "
          >
            <div
              v-if="
                !message.ownMessage &&
                (index == 0 ||
                  chatInfo.messages[index - 1].ownMessage != message.ownMessage)
              "
              class="chat__message__name"
            >
              {{ getFirstName(message.senderName) }}
            </div>
            <div class="chat__message__content">
              {{ message.content }}
            </div>
            <div class="chat__message__time">
              {{ getTime(message.sendingTime) }}
            </div>
          </div>
          <div v-else-if="message.action == 2" class="chat__message--info">
            <b>{{ message.senderName}}</b> created the chat
          </div>
        </div>
      </div>
      <div class="chat__messages__input">
        <input
          type="text"
          v-model="newMessage"
          placeholder="Write your message"
        />
        <button @click="sendMessage">Send</button>
      </div>
    </div>
  </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'

const emit = defineEmits(['inviteUser', 'sendMessage'])

const props = defineProps({
  chatInfo: {
    type: Object,
  },
})

const newMessage = ref('')

const formatDate = (date) => {
  return date.toString().padStart(2, '0')
}

const getTime = (time) => {
  if (time == null) return ''
  const date = new Date(time)
  const today = new Date()
  return `${formatDate(date.getHours())}:${formatDate(date.getMinutes())}`
}

const getFirstName = (name) => {
  let splitted = name.trim().split(' ')
  let firstName = splitted[0]
  if (firstName.length > 10) {
    firstName = firstName.substring(0, 10)
  }
  return firstName
}

const sendMessage = () => {
  if (newMessage.value) {
    emit('sendMessage', newMessage.value)
  }
}
</script>
