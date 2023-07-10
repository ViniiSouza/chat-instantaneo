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
        <button class="chat__empty__container__button">Start a new</button>
      </div>
    </div>
    <div v-else class="chat__messages__container__chat">
      <div class="chat__messages__header">
        <div class="chat__messages__name">{{ chatInfo.title }}</div>
        <div class="chat__messages__info">Available</div>
      </div>
      <div class="chat__messages__area">
        <div
          class="chat__messages__message chat__messages__message--normal"
          v-for="message in chatInfo.messages"
          :key="message.id"
          :class="
            message.ownMessage
              ? 'chat__messages__message--sent'
              : 'chat__messages__message--received'
          "
        >
          <div class="chat__messages__message__time">
            {{ getTime(message.sendingTime) }}
          </div>
          <div class="chat__messages__message__name">
            {{ message.ownMessage ? 'You' : getFirstName(message.senderName) }}
          </div>
          <div class="chat__messages__message--content">
            {{ message.content }}
          </div>
        </div>
      </div>
      <div class="chat__messages__input" @submit.prevent="sendMessage">
        <input
          type="text"
          v-model="newMessage"
          placeholder="Write your message"
        />
        <button>Send</button>
      </div>
    </div>
  </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'

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
  // doNothing
}
</script>
