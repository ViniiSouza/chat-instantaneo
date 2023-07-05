

<template>
  <div class="chat__container">
    <Conversations @conversation-selected="loadConversation" />
    <ChatArea :chat-info="currentChat" />
  </div>
</template>

<script setup>
import './shared/style.css'
import { ref } from 'vue'
import Conversations from './shared/conversations/Conversations.vue'
import ChatArea from './shared/chat-area/ChatArea.vue'
import api from './shared/api'

const currentChat = ref({
  title: 'Loading',
  messages: []
})

const loadConversation = conversation => {
  api.getConversation(conversation.id).then(payload => {
    let result = payload.data
    if (!result.title) result.title = conversation.title
    currentChat.value = result
  })
}
</script>