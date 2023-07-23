<template>
  <h5 class="invite__title">Received invites</h5>
  <hr />
  <ul class="invite__list">
    <li class="invite__list__item">
      <p class="invite__user-info">
        <b>Name</b> <i>(username)</i> sent you a message request:
      </p>
      <p class="invite__user-message">
        "Hi! Could you accept my message request?"
      </p>
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
</script>