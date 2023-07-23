<template>
  <div>
    <h5 class="invite__title">Received invites</h5>
    <hr />
    <div class="invite__search">
      <input
        class="invite__search__input"
        type="search"
        placeholder="Search for a user"
        v-model="userName"
        @input="validateUsername"
      />
      <font-awesome-icon
        class="invite__search__button"
        icon="fa-solid fa-magnifying-glass"
        @click="findUser"
      />
    </div>
    <small v-if="invalidMessage" class="invalid-message">{{
      invalidMessage
    }}</small>
    <div v-if="searchedUser" class="invite__user__container">
      <p class="invite__user__status">
        {{ getTextByType(searchedUser.type) }}
      </p>
      <div class="invite__user__card">
        <p class="invite__user__name">
          {{ searchedUser.name }}&nbsp;<i>({{ searchedUser.userName }})</i>
        </p>
        <div
          v-if="searchedUser.type == 1 || searchedUser.type == 3"
          class="invite__action-button"
          @click="sendMessage(searchedUser)"
        >
          Send message &nbsp;
          <font-awesome-icon icon="fa-regular fa-paper-plane" />
        </div>
        <div
          v-else-if="searchedUser.type == 2"
          class="invite__action-button"
          @click="openInviteBox(searchedUser.userName)"
        >
          Send request &nbsp;
          <font-awesome-icon icon="fa-solid fa-user-plus" />
        </div>
        <div
          v-else-if="searchedUser.type == 4"
          class="invite__action-button"
          @click="openPrivateChat(searchedUser)"
        >
          Open chat &nbsp;
          <font-awesome-icon icon="fa-solid fa-arrow-up-right-from-square" />
        </div>
      </div>
    </div>
    <div v-if="showMessageBox" class="invite-message__box">
      Send a custom message request:
      <font-awesome-icon
        class="invite-message__message__close-button"
        icon="fa-solid fa-xmark"
        @click="showMessageBox = false"
      />
      <div class="invite-message__message__container">
        <textarea
          maxlength="80"
          class="invite-message__box__input"
          type="textarea"
          v-model="customRequest"
        ></textarea>
        <button class="invite-message__send-button" @click="sendInvite">
          <font-awesome-icon icon="fa-regular fa-paper-plane" />
        </button>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref } from 'vue'
import api from '../../../api'
import { useToast } from 'vue-toastification'

const toast = useToast()

const emit = defineEmits(['openPrivateChat', 'createTempChat'])

const userName = ref('')
const lastValidUser = ref('')

const showMessageBox = ref(false)
const searchedUser = ref(null)
const invalidMessage = ref('')
const customRequest = ref('Hi! Could you accept my message request?')

document.addEventListener('click', (event) => {
  if (
    showMessageBox.value == true &&
    event.target.matches('.modal__container')
  ) {
    showMessageBox.value = false
  }
})

const getTextByType = (type) => {
  switch (type) {
    case 0:
      return ''
    case 1:
      return 'This user has a public profile and can receive messages from anyone'
    case 2:
      return 'This user has a private profile. Do you want to send a message request?'
    case 3:
      return 'You have permission to send a message to this user.'
    case 4:
      return 'You already have a chat with this user. Do you want to open it?'
    case 5:
      return 'You already sent a message request to this user. You need to wait for the user to accept it.'
    default:
      return ''
  }
}

const sendMessage = (user) => {
  emit('createTempChat', user)
}

const openInviteBox = (userName) => {
  showMessageBox.value = true
  lastValidUser.value = userName
}

const sendInvite = () => {
  const messageInfo = {
    receiver: lastValidUser.value,
    message: customRequest.value,
  }
  api
    .sendMessageRequest(messageInfo)
    .then((payload) => {
      if (payload.status == 201) {
        toast.success('Message request sent!')
        showMessageBox.value = false
      }
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        toast.error(err.response.data)
      } else {
        const errorMsg = 'Something went wrong. Try again later.'
        toast.error(errorMsg)
      }
    })
}

const openPrivateChat = (user) => {
  emit('openPrivateChat', user)
}

const validateUsername = () => {
  if (userName.value) {
    invalidMessage.value = ''
    return true
  }
  invalidMessage.value = 'Enter the username'
  return false
}

const findUser = () => {
  if (validateUsername()) {
    api
      .searchUser(userName.value)
      .then((payload) => {
        if (payload.status == 200) {
          searchedUser.value = payload.data
        }
      })
      .catch((err) => {
        if (err.response && err.response.data) {
          invalidMessage.value = err.response.data
        } else {
          const errorMsg = 'Something went wrong. Try again later.'
          toast.error(errorMsg)
        }
      })
  }
}
</script>