<template>
  <div class="chat__container">
    <Conversations
      :conversations="conversations"
      :selected-conversation="selectedConversation"
      @conversation-selected="loadConversation"
      @menu-option="openMenuOption"
      @logout="logout"
    />
    <ChatArea
      :chat-info="currentChat"
      @invite-user="openMenuOption(2)"
      @send-message="sendMessageFromChat"
    />
    <modal v-if="showModal" :title="modalTitle" @close="showModal = false">
      <template #body>
        <div v-if="currentModal == 1">
          <Contacts @send-message-to-contact="sendMessageToContact" />
        </div>
        <div v-else-if="currentModal == 2">
          <Invite
            @open-private-chat="openPrivateChat"
            @create-temp-chat="createTempChat"
          />
        </div>
        <div v-else-if="currentModal == 3">Configurations</div>
      </template>
    </modal>
  </div>
</template>

<script setup>
import './shared/style.css'
import { onBeforeUnmount, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import useChat from './useChat'

// Components
import Conversations from './shared/conversations/Conversations.vue'
import ChatArea from './shared/chat-area/ChatArea.vue'
import Modal from '../shared/modal/Modal.vue'
import Contacts from './shared/contacts/Contacts.vue'
import Invite from './shared/invite/Invite.vue'

const router = useRouter()
const toast = useToast()

const currentChat = ref(null)
const selectedConversation = ref(null)
const hasDraft = ref(false)
const conversations = ref([])
const modalTitle = ref('')
const showModal = ref(false)
const currentModal = ref(0)

const {
  openMenuOption,
  createTempChat,
  loadConversation,
  sendMessageToContact,
  sendMessageFromChat,
  openPrivateChat,
  logout,
  hub,
} = useChat(
  router,
  toast,
  currentChat,
  selectedConversation,
  hasDraft,
  conversations,
  modalTitle,
  showModal,
  currentModal,
  onBeforeUnmount
)
</script>