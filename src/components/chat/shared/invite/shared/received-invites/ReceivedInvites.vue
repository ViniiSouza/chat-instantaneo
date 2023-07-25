<template>
  <h5 class="invite__title">Received invites</h5>
  <hr />
  <ul v-if="requests && requests.length > 0" class="invite__list">
    <li
      v-for="request in requests"
      :key="request.requester?.id"
      class="invite__list__item"
    >
      <p class="invite__user-info">
        <b>{{ request.requester?.name }}</b>
        <i>({{ request.requester?.userName }})</i> sent you a message request:
      </p>
      <p class="invite__user-message">"{{ request.message }}"</p>
      <div class="invite__actions__container">
        <button
          class="invite__action--accept"
          @click="acceptRequest(request.id)"
        >
          <font-awesome-icon icon="fa-solid fa-check" />Accept
        </button>
        <button
          class="invite__action--refuse"
          @click="refuseRequest(request.id)"
        >
          <font-awesome-icon icon="fa-solid fa-xmark" />Refuse
        </button>
      </div>
    </li>
  </ul>
  <div class="text-center">You don't have any requests</div>
</template>
<script setup>
import { ref } from 'vue'
import api from '../../../api'

const emit = defineEmits(['acceptRequest', 'refuseRequest'])
const requests = ref([])

const loadRequests = () => {
  api.getRequestsFromUser().then((payload) => {
    requests.value = payload.data
  })
}

loadRequests()

const reloadRequests = () => {
  loadRequests()
}

const acceptRequest = (requestId) => {
  emit('acceptRequest', requestId)
}

const refuseRequest = (requestId) => {
  emit('refuseRequest', requestId)
}

defineExpose({
  reloadRequests,
})
</script>