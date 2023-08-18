<template>
  <div class="invite__container">
    <div class="invite__change-option__wrapper">
      <button
        class="invite__change-option"
        @click="showSendInvite = !showSendInvite"
      >
        {{ showSendInvite ? 'View incoming requests' : 'Send request' }}
      </button>
    </div>
    <SendInvite
      v-if="showSendInvite"
      @create-temp-chat="sendMessage"
      @open-private-chat="openPrivateChat"
    />
    <ReceivedInvites
      v-else
      ref="SendInviteCp"
      @accept-request="acceptRequest"
      @refuse-request="refuseRequest"
    />
  </div>
</template>
<script setup>
import './shared/style.css'
import SendInvite from './shared/send-invite/SendInvite.vue'
import ReceivedInvites from './shared/received-invites/ReceivedInvites.vue'
import { ref } from 'vue'

const emit = defineEmits([
  'openPrivateChat',
  'createTempChat',
  'acceptRequest',
  'refuseRequest',
])

const SendInviteCp = ref(null)

const showSendInvite = ref(false)

const sendMessage = (user) => {
  emit('createTempChat', user)
}

const openPrivateChat = (user) => {
  emit('openPrivateChat', user)
}

const acceptRequest = (requestId) => {
  emit('acceptRequest', requestId)
}

const refuseRequest = (requestId) => {
  emit('refuseRequest', requestId)
}

const reloadRequests = () => {
  SendInviteCp.value.reloadRequests()
}

defineExpose({
  reloadRequests,
})
</script>