<template>
  <div class="chat__container">
    <Conversations
      :conversations="conversations"
      :selected-conversation="selectedConversation"
      @conversation-selected="loadConversation"
      @menu-option="openMenuOption"
      @logout="logout"
    />
    <ChatArea :chat-info="currentChat" />
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
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Conversations from './shared/conversations/Conversations.vue'
import ChatArea from './shared/chat-area/ChatArea.vue'
import Modal from '../shared/modal/Modal.vue'
import Contacts from './shared/contacts/Contacts.vue'
import Invite from './shared/invite/Invite.vue'
import api from './shared/api'
import { useToast } from 'vue-toastification'

const router = useRouter()

const toast = useToast()

const currentChat = ref(null)
const selectedConversation = ref(null)
const hasDraft = ref(false)

const conversations = ref([])

const modalTitle = ref('')
const showModal = ref(false)

const currentModal = ref(0)

const openMenuOption = (optionValue) => {
  showModal.value = true
  currentModal.value = optionValue
  switch (optionValue) {
    case 1:
      modalTitle.value = 'Contacts'
      break
    case 2:
      modalTitle.value = 'Invite user'
      break
  }
}

const conversationSelected = (conversation) => {
  if (selectedConversation.value != conversation.id) {
    selectedConversation.value = conversation.id
  }
}

const loadAllConversations = () => {
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
}

// on each first load
loadAllConversations()

const createTempChat = (user) => {
  removeDraft()
  const tempChat = {
    id: -1,
    lastMessage: {
      content: 'Draft',
      ownMessage: false,
    },
    title: user.name,
    type: 1,
    draft: true,
  }
  conversations.value.unshift(tempChat)
  loadConversation(conversations.value[0])
  hasDraft.value = true
  showModal.value = false
}

const removeDraft = () => {
  if (hasDraft.value) {
    conversations.value.shift()
    hasDraft.value = false
  }
}

const loadConversation = (conversation) => {
  conversationSelected(conversation)
  removeDraft()
  if (conversation.draft) {
    currentChat.value = {
      messages: [],
      title: conversation.title,
      type: 1,
    }
  } else {
    api
      .getConversation(conversation.id)
      .then((payload) => {
        let result = payload.data
        if (!result.title) result.title = conversation.title
        currentChat.value = result
      })
      .catch((err) => {
        if (err.response && err.response.data) {
          toast.error(err.response.data)
        } else {
          const errorMsg = 'Unable to load this conversation. Try again later.'
          toast.error(errorMsg)
        }
      })
  }
}

const sendMessageToContact = target => {
  api.findPrivateConversation(target.userName).then(payload => {
    if (payload.status == 200) {
      loadConversation(payload.data)
        showModal.value = false
    }
  }).catch(err => {
    if (err.response.status == 404) {
      createTempChat(target)
    } else {
      const errorMsg = 'Something went wrong. Try again later.'
      toast.error(errorMsg)
    }
  })
}

const openPrivateChat = (targetUserName) => {
  api
    .findPrivateConversation(targetUserName)
    .then((payload) => {
      if (payload.status == 200) {
        loadConversation(payload.data)
        showModal.value = false
      }
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        toast.error(err.response.data)
      } else {
        const errorMsg = 'Unable to find this conversation. Try again later.'
        toast.error(errorMsg)
      }
    })
}

const logout = () => {
  router.push({ name: 'login' })
}
</script>