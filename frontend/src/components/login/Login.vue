<template>
  <FormComponent>
    <div class="login__form-group">
      <label class="login__form--label" for="username">Username:</label>
      <input
        class="login__form--input"
        :class="!validator.userName.valid ? 'invalid-input' : ''"
        type="text"
        maxlength="15"
        v-model="user.userName"
        placeholder="Enter your username"
        @blur="validateUser"
        @input="validateUser"
      />
      <span v-if="!validator.userName.valid" class="invalid-message">{{
        validator.userName.message
      }}</span>
    </div>
    <div class="login__form-group">
      <label class="login__form--label" for="password">Password:</label>
      <input
        class="login__form--input"
        :class="!validator.password.valid ? 'invalid-input' : ''"
        type="password"
        v-model="user.password"
        placeholder="Enter your password"
        @blur="validatePassword"
        @input="validatePassword"
      />
      <span v-if="!validator.password.valid" class="invalid-message">{{
        validator.password.message
      }}</span>
    </div>
    <button id="login__form--sign-in" @click="signIn">Sign in</button>
    <p>
      Don't have an account?
      <a href="#" class="login__form--sign-up" @click="toSignUp">Sign up</a>
    </p>
  </FormComponent>
</template>


<script>
import FormComponent from '../shared/form-section/FormComponent.vue'
import './shared/style.css'

export default {
  data() {
    return {
      user: {
        userName: '',
        password: '',
      },
      validator: {
        userName: {
          message: '',
          valid: true,
        },
        password: {
          message: '',
          valid: true,
        },
      },
    }
  },
  components: {
    FormComponent,
  },
  methods: {
    signIn() {
      if (this.validate()) {
        // sign in
      }
    },
    toSignUp() {
      this.$router.push({ name: 'register' })
    },
    validate() {
      let valid = true
      if (!this.validateUser())
        valid = false
      if (!this.validatePassword())
        valid = false
      return valid
    },
    validateUser() {
      let valid = true
      if (!this.user.userName || this.user.userName.length == 0) {
        this.validator.userName.valid = false
        this.validator.userName.message = 'Invalid username'
        valid = false
      }
      else {
        this.validator.userName.valid = true
      }

      return valid
    },
    validatePassword() {
      let valid = true
      if (!this.user.password || this.user.password.length == 0) {
        this.validator.password.valid = false
        this.validator.password.message = 'Invalid password'
        valid = false
      }
      else {
        this.validator.password.valid = true
      }

      return valid
    }
  },
}
</script>