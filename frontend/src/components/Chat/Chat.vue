

<template>
  <div class="chat__container">
    <div class="chat__conversations__container">
      <div class="chat__conversations__header">
        <h2>Conversations</h2>
        <input type="text" v-model="newMessage" placeholder="Search" />
      </div>
      <ul class="chat__conversations__list">
        <li class="chat__conversations__item">
          <p class="chat__conversations__item--time">xx:xx</p>
          <p class="chat__conversations__item--name">*User*</p>
          *last message*
        </li>
      </ul>
    </div>
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
  </div>
</template>

<script setup>
import './shared/style.css'
</script>