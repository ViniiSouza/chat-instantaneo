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
        <div class="chat__messages__info">{{ chatInfo.type == 1 ? getUserStatus : 'Not private' }}</div>
      </div>
      <div class="chat__messages__area" ref="messageArea">
        <div
          class="chat__messages__wrapper"
          v-for="(message, index) in chatInfo.messages"
          :key="message.id"
        >
          <div
            v-if="message.action == 1"
            class="chat__message--text"
            :class="
              message.ownMessage
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
              {{ dateHandler.getStringTime(message.sendingTime) }}
            </div>
          </div>
          <div v-else-if="message.action == 2" class="chat__message--info">
            <b>{{ message.senderName }}</b> created the chat
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
import { computed, nextTick, ref } from 'vue'
import dateHandler from '@/utils/dateHandler.js'

const emit = defineEmits(['inviteUser', 'sendMessage'])

const props = defineProps({
  chatInfo: {
    type: Object,
  },
})

const messageArea = ref(null)
const newMessage = ref('')

const getFirstName = (name) => {
  let splitted = name.trim().split(' ')
  let firstName = splitted[0]
  if (firstName.length > 10) {
    firstName = firstName.substring(0, 10)
  }
  return firstName
}

const getUserStatus = computed(() => {
  if (props.chatInfo.status) {
    if (props.chatInfo.status.status == 1)
      return 'On-line'
    if (dateHandler.dateIsToday(props.chatInfo.status.lastSeen))
      return `Last seen today at ${dateHandler.getStringTime(props.chatInfo.status.lastSeen)}`
    if (dateHandler.dateIsYesterday(props.chatInfo.status.lastSeen))
      return `Last seen yesterday at ${dateHandler.getStringTime(props.chatInfo.status.lastSeen)}`
    
    return `Last seen ${dateHandler.getStringDate(props.chatInfo.status.lastSeen)}`
  }
})

const sendMessage = () => {
  if (newMessage.value) {
    emit('sendMessage', newMessage.value)
    newMessage.value = ''
  }
}

const userInBottom = computed(
  () =>
    messageArea.value.scrollTop >=
    messageArea.value.scrollHeight - messageArea.value.offsetHeight
)

const scrollToBottom = () => {
  nextTick(() => {
    messageArea.value.scrollTop = messageArea.value.scrollHeight
  })
}

defineExpose({
  userInBottom,
  scrollToBottom,
})
</script>
