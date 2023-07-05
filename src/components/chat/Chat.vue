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
import { useToast } from 'vue-toastification'

const toast = useToast()

const currentChat = ref(null)

const loadConversation = conversation => {
  api.getConversation(conversation.id).then(payload => {
    let result = payload.data
    if (!result.title) result.title = conversation.title
    currentChat.value = result
  }).catch((err) => {
    if (err.response && err.response.data) {
      toast.error(err.response.data)
    } else {
      const errorMsg = 'Unable to load this conversation. Try again later.'
      toast.error(errorMsg)
    }
  })
}
</script>