<template>
  <div class="invite__container">
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
      <p class="invite__user__status">{{ getTextByType(searchedUser.type) }}</p>
      <div class="invite__user__card">
        <p class="invite__user__name">
          {{ searchedUser.name }}&nbsp;<i>({{ searchedUser.userName }})</i>
        </p>
        <div
          v-if="searchedUser.type == 1 || searchedUser.type == 3"
          class="invite__action-button"
          @click="sendMessage(searchedUser.id)"
        >
          Send message &nbsp;
          <font-awesome-icon icon="fa-regular fa-paper-plane" />
        </div>
        <div v-else-if="searchedUser.type == 2" class="invite__action-button"
        @click="sendInvite(searchedUser)">
          Send request &nbsp;
          <font-awesome-icon icon="fa-solid fa-user-plus" />
        </div>
        <div v-else-if="searchedUser.type == 4" class="invite__action-button"
        @click="openPrivateChat(searchedUser.userName)">
          Open chat &nbsp;
          <font-awesome-icon icon="fa-solid fa-arrow-up-right-from-square" />
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'
import api from '../../../chat/shared/api'

const emit = defineEmits(['openPrivateChat'])

const userName = ref('')

const searchedUser = ref(null)
const invalidMessage = ref('')

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
    default:
      return ''
  }
}

const sendMessage = id => {
  // do something
}

const sendInvite = user => {
  // do something
}

const openPrivateChat = userName => {
  emit('openPrivateChat', userName)
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
    api.searchUser(userName.value).then((payload) => {
      if (payload.status == 200) {
        searchedUser.value = payload.data
      }
    }).catch((err) => {
      if (err.response && err.response.data) {
        invalidMessage.value = err.response.data
      }
    })
  }
}
</script>