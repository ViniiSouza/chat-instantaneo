<template>
  <div class="chat__messages__container">
      <div class="chat__messages__header">
        <div class="chat__messages__name">*User*</div>
        <div class="chat__messages__info">*status*</div>
      </div>
      <div class="chat__messages__area">
        <div
          class="chat__messages__message chat__messages__message--received chat__messages__message--large"
        >
          <div class="chat__messages__message__time">xx:xx</div>
          <div class="chat__messages__message__name">*User*</div>
          <div class="chat__messages__message--content">**large message**</div>
        </div>
        <div
          class="chat__messages__message chat__messages__message--sent chat__messages__message--normal"
        >
          <div class="chat__messages__message__time">xx:xx</div>
          <div class="chat__messages__message__name">You</div>
          <div class="chat__messages__message__content">**normal message**</div>
        </div>
      </div>
      <div class="chat__messages__input" @submit.prevent="sendMessage">
        <input
          type="text"
          v-model="newMessage"
          placeholder="Write your message"
        />
        <button>Send</button>
      </div>
    </div>
</template>
<script setup>
import './shared/style.css'
</script>
