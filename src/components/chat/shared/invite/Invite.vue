<template>
  <div>
    <SendInvite
      @create-temp-chat="sendMessage"
      @open-private-chat="openPrivateChat"
    />
  </div>
</template>
<script setup>
import './shared/style.css'
import SendInvite from './shared/send-invite/SendInvite.vue'

const emit = defineEmits(['openPrivateChat', 'createTempChat'])

const sendMessage = (user) => {
  emit('createTempChat', user)
}

const openPrivateChat = (user) => {
  emit('openPrivateChat', user)
}
</script>