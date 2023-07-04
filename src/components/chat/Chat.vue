

<template>
  <div class="chat__container">
    <Conversations />
    <ChatArea />
  </div>
</template>

<script setup>
import './shared/style.css'
import Conversations from './shared/conversations/Conversations.vue'
import ChatArea from './shared/chat-area/ChatArea.vue'
</script>