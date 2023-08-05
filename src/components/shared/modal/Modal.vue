<template>
  <transition name="modal">
    <div class="modal__mask">
      <div class="modal__container">
        <div class="modal__header">
          <slot name="header">
            <h3>{{ title }}</h3>
          </slot>
        </div>
        <div class="modal__body">
          <slot name="body">Default body</slot>
        </div>
        <div class="modal__footer">
          <slot name="footer">
            <div class="modal__footer_buttons-container">
              <button class="modal__default-button" @click="emit('close')">
                Cancel
              </button>
              <button class="modal__default-button" @click="emit('close')">
                OK
              </button>
            </div>
          </slot>
        </div>
      </div>
    </div>
  </transition>
</template>
<script setup>
import './shared/style.css'

const props = defineProps({
  title: {
    type: String,
    default: 'Modal Title',
  },
})

const emit = defineEmits(['close'])

document.addEventListener('click', (event) => {
  if (event.target.matches('.modal__wrapper')) {
    emit('close')
  }
})

document.addEventListener('keydown', (event) => {
  if (event.key == 'Escape') {
    emit('close')
  }
})
</script>