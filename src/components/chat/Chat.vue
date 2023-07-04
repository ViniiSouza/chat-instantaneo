

<template>
  <div class="chat__container">
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
        <li
          v-for="conversation in conversations"
          :key="conversation.id"
          class="chat__conversations__item"
        >
          <p class="chat__conversations__item--time">
            {{ getTime(conversation.lastMessage?.sendingTime) }}
          </p>
          <p class="chat__conversations__item--name">
            {{ conversation.title }}
          </p>
          <span v-if="conversation.lastMessage && conversation.type == 1">
            <font-awesome-icon
              v-if="conversation.lastMessage?.ownMessage"
              icon="fa-solid fa-check"
            />
            {{ conversation.lastMessage?.content }}
          </span>
        </li>
      </ul>
    </div>
    <div class="chat__messages__container">
      <div class="chat__messages__header">
        <div class="chat__messages__name">*User*</div>
        <div class="chat__messages__info">*status*</div>
      </div>
      <div class="chat__messages__area">
        <div
          class="chat__messages__message chat__messages__message--received chat__messages__message--large"
        >
          <div class="chat__messages__message__time">xx:xx</div>
          <div class="chat__messages__message__name">*User*</div>
          <div class="chat__messages__message--content">**large message**</div>
        </div>
        <div
          class="chat__messages__message chat__messages__message--sent chat__messages__message--normal"
        >
          <div class="chat__messages__message__time">xx:xx</div>
          <div class="chat__messages__message__name">You</div>
          <div class="chat__messages__message__content">**normal message**</div>
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
import api from './shared/api.js'
import { useToast } from 'vue-toastification'

const toast = useToast()

const conversations = ref([])

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
</script>