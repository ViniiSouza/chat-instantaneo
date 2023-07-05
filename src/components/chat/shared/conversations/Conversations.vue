<template>
  <div class="chat__conversations__container">
      <div class="chat__conversations__header">
        <div class="chat__conversations__header__options">
          <span class="chat__conversations__header__icon">
            <font-awesome-icon icon="fa-solid fa-pen-to-square" />
          </span>
          <span class="chat__conversations__header__icon"> 
            <font-awesome-icon icon="fa-solid fa-user-plus" />
          </span>
          <span class="chat__conversations__header__icon">
            <font-awesome-icon icon="fa-solid fa-gear" />
          </span>
        </div>
        <h2>Conversations</h2>
        <input type="text" v-model="newMessage" placeholder="Search" />
      </div>
      <ul class="chat__conversations__list">
        <li v-for="conversation in conversations" :key="conversation.id" class="chat__conversations__item" :class="selectedConversation == conversation.id && 'chat__conversations__item--selected'" @click="selectConversation(conversation)">
          <p class="chat__conversations__item__time">{{ getTime(conversation.lastMessage?.sendingTime) }}</p>
          <p class="chat__conversations__item__name">{{ conversation.title }}</p>
          <span v-if="conversation.lastMessage && conversation.type == 1">
            <b v-if="conversation.lastMessage?.ownMessage">You: </b> {{ conversation.lastMessage?.content }}
          </span>
        </li>
      </ul>
    </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'
import api from '../api.js'
import { useToast } from 'vue-toastification'

const emit = defineEmits(['conversationSelected'])

const toast = useToast()

const conversations = ref([])

const selectedConversation = ref(null)

api
  .loadAll()
  .then((response) => {
    if (response.status == 200) {
      conversations.value = response.data
    }
  })
  .catch((err) => {
    if (err.response && err.response.data) {
      toast.error(err.response.data)
    } else {
      const errorMsg = 'Unable to load your conversations. Try again later.'
      toast.error(errorMsg)
    }
  })

const formatDate = (date) => {
  return date.toString().padStart(2, '0')
}

const getTime = (time) => {
  if (time == null) return ''
  const date = new Date(time)
  const today = new Date()
  if (date.toDateString() == today.toDateString()) {
    return `${formatDate(date.getHours())}:${formatDate(date.getMinutes())}`
  }
  return `${formatDate(date.getDate())}/${formatDate(
    date.getMonth()
  )}/${date.getFullYear()}`
}

const selectConversation = (conversation) => {
  if (selectedConversation.value != conversation.id) {
    selectedConversation.value = conversation.id
    emit('conversationSelected', conversation)
  }
}
</script>