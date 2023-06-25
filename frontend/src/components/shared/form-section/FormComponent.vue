<template>
<div class="form__container">
  <div class="form__card">
    <div v-if="showLogo" class="form__logo--container">
        <img
          class="form__logo--image"
          :src="logoImg"
          alt="Logo"
        />
      </div>
      <form>
        <slot></slot>
      </form>
  </div>
</div>
</template>
<script>
import './shared/style.css'
export default {
  data() { 
    return {

    }
  },
  props: {
    showLogo: {
      type: Boolean,
      default: true
    },
    logoImg: {
      type: String,
      default: 'https://i.imgur.com/zBJnlSa.png'
    }
  }
}
</script>
