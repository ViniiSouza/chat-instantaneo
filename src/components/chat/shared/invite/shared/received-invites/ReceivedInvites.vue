<template>
  <h5 class="invite__title">Received invites</h5>
  <hr />
  <ul class="invite__list">
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
        <button class="invite__action--accept">
          <font-awesome-icon icon="fa-solid fa-check" />Accept
        </button>
        <button class="invite__action--refuse">
          <font-awesome-icon icon="fa-solid fa-xmark" />Refuse
        </button>
      </div>
    </li>
  </ul>
</template>
<script setup>
import { ref } from 'vue'
import api from '../../../api'

const requests = ref([])

api.getRequestsFromUser().then((payload) => {
  requests.value = payload.data
})
</script>