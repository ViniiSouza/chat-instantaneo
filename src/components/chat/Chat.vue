<template>
  <div class="chat__container">
    <Conversations @conversation-selected="loadConversation" @menu-option="openMenuOption" />
    <ChatArea :chat-info="currentChat" />
    <modal v-if="showModal" @close="showModal = false">
      <template #body>
        <div v-if="currentModal == 1">
          <Contacts/>
        </div>
        <div v-else-if="currentModal == 2">
          <Invite @open-private-chat="openPrivateChat"/>
        </div>
        <div v-else-if="currentModal == 3">
          Configurations
        </div>
      </template>
    </modal>
  </div>
</template>

<script setup>
import './shared/style.css'
import { ref } from 'vue'
import Conversations from './shared/conversations/Conversations.vue'
import ChatArea from './shared/chat-area/ChatArea.vue'
import Modal from '../shared/modal/Modal.vue'
import Contacts from './shared/contacts/Contacts.vue'
import Invite from './shared/invite/Invite.vue'
import api from './shared/api'
import { useToast } from 'vue-toastification'

const toast = useToast()

const currentChat = ref(null)

const showModal = ref(false)

const currentModal = ref(0)

const openMenuOption = optionValue => {
  showModal.value = true
  currentModal.value = optionValue
}

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

const openPrivateChat = targetUserName => {
  api.findPrivateConversation(targetUserName).then(payload => {
    if (payload.status == 200) {
      loadConversation(payload.data)
      showModal.value = false
    }
  })
}

</script>