<template>
  <FormComponent>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Username:</label>
      <input
        class="register__form--input"
        type="text"
        id="username"
        v-model="username"
        placeholder="Enter your username"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Name:</label>
      <input
        class="register__form--input"
        type="text"
        id="name"
        v-model="name"
        placeholder="Enter your name"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="password">Password:</label>
      <input
        class="register__form--input"
        type="password"
        id="password"
        v-model="password"
        placeholder="Enter your password"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="password">Confirm Password:</label>
      <input
        class="register__form--input"
        type="password"
        id="confirm-password"
        v-model="confirmPassword"
        placeholder="Confirm your password"
      />
    </div>
    <button id="register__form--sign-in" @click="signUp">Sign up</button>
    <p>
      Already have an account?
      <a href="#" class="register__form--sign-up" @click="toSignIn">Sign in</a>
    </p>
  </FormComponent>
</template>
<script>
import './shared/style.css'

import FormComponent from '../shared/form-section/FormComponent.vue'

export default {
  data() {
    return {}
  },
  components: {
    FormComponent,
  },
  methods: {
    signUp() {

    },
    toSignIn() {
      this.$router.push({ name: 'login' })
    }
  }
}
</script>
