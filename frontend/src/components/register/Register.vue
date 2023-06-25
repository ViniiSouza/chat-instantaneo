<template>
  <FormComponent>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Username:</label>
      <input
        id="username"
        class="register__form--input"
        type="text"
        v-model="user.userName"
        placeholder="Enter your username"
        maxlength="15"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Name:</label>
      <input
        id="name"
        class="register__form--input"
        type="text"
        v-model="user.name"
        placeholder="Enter your name"
        maxlength="40"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="password">Password:</label>
      <input
        class="register__form--input"
        type="password"
        id="password"
        v-model="user.password"
        placeholder="Enter your password"
      />
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="password"
        >Confirm Password:</label
      >
      <input
        class="register__form--input"
        type="password"
        id="confirm-password"
        v-model="user.passwordConfirm"
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
    return {
      user: {
        userName: '',
        name: '',
        password: '',
        passwordConfirm: '',
      },
    }
  },
  components: {
    FormComponent,
  },
  methods: {
    signUp() {},
    toSignIn() {
      this.$router.push({ name: 'login' })
    },
  },
}
</script>
