<template>
  <div class="chat__container">
    <Conversations
      :conversations="conversations"
      :selected-conversation="currentChat ? currentChat.id : null"
      :is-mobile="isMobile"
      @conversation-selected="loadConversation"
      @menu-option="openMenuOption"
      @logout="logout"
    />
    <ChatArea
      ref="ChatAreaCp"
      :chat-info="currentChat"
      :is-mobile="isMobile"
      @invite-user="openMenuOption(2)"
      @send-message="sendMessageFromChat"
      @back-to-menu="clearCurrentChat"
      @add-contact="addContact"
      @remove-contact="removeContact"
      @load-messages="loadMessages"
    />
    <modal v-if="showModal" :title="modalTitle" @close="showModal = false">
      <template #body>
        <div v-if="currentModal == 1">
          <Contacts @send-message-to-contact="sendMessageToContact" />
        </div>
        <div v-else-if="currentModal == 2">
          <Invite
            ref="InviteCp"
            @open-private-chat="openPrivateChat"
            @create-temp-chat="createTempChat"
            @accept-request="acceptRequest"
            @refuse-request="refuseRequest"
          />
        </div>
        <div v-else-if="currentModal == 3">Configurations</div>
      </template>
    </modal>
    <audio style="display: none" ref="NotificationSoundEl" src="src/assets/audio/message_notification.wav" ></audio>
  </div>
</template>

<script setup>
import './shared/style.css'
import { computed, onBeforeUnmount, ref } from 'vue'
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

const NotificationSoundEl = ref()

const ChatAreaCp = ref(null)
const InviteCp = ref(null)

const currentChat = ref(null)
const hasDraft = ref(false)
const conversations = ref([])
const modalTitle = ref('')
const showModal = ref(false)
const currentModal = ref(0)

const isMobile = computed(() => window.innerWidth <= 768)

const {
  openMenuOption,
  createTempChat,
  loadConversation,
  sendMessageToContact,
  sendMessageFromChat,
  openPrivateChat,
  logout,
  hub,
  clearCurrentChat,
  acceptRequest,
  refuseRequest,
  addContact,
  removeContact,
  loadMessages
} = useChat(
  router,
  toast,
  currentChat,
  hasDraft,
  conversations,
  modalTitle,
  showModal,
  currentModal,
  onBeforeUnmount,
  ChatAreaCp,
  InviteCp,
  NotificationSoundEl
)
</script>