<template>
  <div class="contacts__container">
    <input
      class="contacts__search"
      type="search"
      placeholder="Search for a contact name"
    />
    <hr />
    <div v-if="contacts && contacts.length > 0" class="contacts__list">
      <div
        class="contacts__item"
        v-for="(contact, index) in contacts"
        :key="contact.id"
      >
        <p class="contacts__item__name">{{ contact.name }}</p>
        <div class="contacts__item__options">
          <div class="tooltip-container" @click="removeContact(contact, index)">
            <font-awesome-icon
              class="contacts__icon contacts__icon--remove"
              icon="fa-regular fa-square-minus"
            />
            <span class="tooltip-component tooltip--bottom"
              >Remove contact</span
            >
          </div>
          <div class="tooltip-container" @click="sendMessage(contact)">
            <font-awesome-icon
              class="contacts__icon contacts__icon--send"
              icon="fa-regular fa-paper-plane"
            />
            <span class="tooltip-component tooltip--bottom">Send message</span>
          </div>
        </div>
      </div>
    </div>
    <div style="text-align: center" v-else>You have no contacts</div>
  </div>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'
import api from '../api'
import Swal from 'sweetalert2'
import swalConfig from '../../../shared/sweetalert/globalConfig'

import { useToast } from 'vue-toastification'

const emit = defineEmits(['sendMessageToContact'])

const toast = useToast()

const contacts = ref([])

const sendMessage = (contact) => {
  emit('sendMessageToContact', contact)
}

const getContacts = () => {
  api
    .getContacts()
    .then((payload) => {
      if (payload.status == 200) {
        contacts.value = payload.data
      }
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        toast.error(err.response.data.message)
      } else {
        const errorMsg = 'Unable to load your contacts. Try again later.'
        toast.error(errorMsg)
      }
    })
}

getContacts()

const removeContact = (contact, index) => {
  Swal.fire({
    ...swalConfig,
    title: `Attention`,
    html: `Do you really want to remove <b>${contact.userName}</b> from your contacts?`,
    showDenyButton: true,
    confirmButtonText: 'Yes',
    denyButtonText: 'No',
  })
    .then((result) => {
      if (result.isConfirmed) {
        api.removeContact(contact.id).then((payload) => {
          if (payload.status == 200) {
            toast.success('Succesfully deleted contact.')
            contacts.value.splice(index, 1)
          }
        })
      }
    })
    .catch((err) => {
      if (err.response && err.response.data) {
        toast.error(err.response.data.message)
      } else {
        const errorMsg = 'Unable to load your contacts. Try again later.'
        toast.error(errorMsg)
      }
    })
}
</script>