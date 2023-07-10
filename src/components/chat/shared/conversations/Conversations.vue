<template>
  <div class="chat__conversations__container">
    <div class="chat__conversations__header">
      <div class="chat__conversations__header__options">
        <span
          class="chat__conversations__header__icon tooltip-container"
          @click="selectOption(1)"
        >
          <font-awesome-icon icon="fa-solid fa-pen-to-square" />
          <span class="tooltip-component tooltip--bottom">Contacts</span>
        </span>
        <span
          class="chat__conversations__header__icon tooltip-container"
          @click="selectOption(2)"
        >
          <font-awesome-icon icon="fa-solid fa-user-plus" />
          <span class="tooltip-component tooltip--bottom">Invite user</span>
        </span>
        <span
          class="chat__conversations__header__icon tooltip-container"
          @click="selectOption(3)"
        >
          <font-awesome-icon icon="fa-solid fa-gear" />
          <span class="tooltip-component tooltip--bottom">Configurations</span>
        </span>
      </div>
      <h2>Conversations</h2>
      <input type="search" v-model="searchValue" placeholder="Search" />
    </div>
    <ul class="chat__conversations__list">
      <li
        v-for="conversation in conversations"
        :key="conversation.id"
        class="chat__conversations__item"
        :class="
          (selectedConversation == conversation.id || conversation.id == -1) &&
          'chat__conversations__item--selected'
        "
        @click="selectConversation(conversation)"
      >
        <p class="chat__conversations__item__time">
          {{ getTime(conversation.lastMessage?.sendingTime) }}
        </p>
        <p class="chat__conversations__item__name">{{ conversation.title }}</p>
        <span
          v-if="conversation.lastMessage && conversation.type == 1"
          :class="
            conversation.draft && 'chat__conversations__item__name--italic'
          "
        >
          <b v-if="conversation.lastMessage?.ownMessage">You: </b>
          {{ conversation.lastMessage?.content }}
        </span>
      </li>
    </ul>
  </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'

const emit = defineEmits(['conversationSelected', 'menuOption'])

const props = defineProps({
  conversations: {
    type: Array,
    default: [],
  },
  selectedConversation: {
    type: Number,
    default: null,
  },
})

const searchValue = ref('')

const selectOption = (optionNumber) => {
  emit('menuOption', optionNumber)
}

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
  emit('conversationSelected', conversation)
}
</script>