<template>
  <FormComponent>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Username:</label>
      <input
        class="register__form--input"
        :class="!validator.userName.valid ? 'invalid-input' : ''"
        type="text"
        v-model="user.userName"
        placeholder="Enter your username"
        maxlength="15"
        @blur="validateUsername"
        @input="validateUsername"
      />
      <span v-if="!validator.userName.valid" class="invalid-message">{{
        validator.userName.message
      }}</span>
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="username">Name:</label>
      <input
        class="register__form--input"
        :class="!validator.name.valid ? 'invalid-input' : ''"
        type="text"
        v-model="user.name"
        placeholder="Enter your name"
        maxlength="40"
        @blur="validateName"
        @input="validateName"
      />
      <span v-if="!validator.name.valid" class="invalid-message">{{
        validator.name.message
      }}</span>
    </div>
    <div class="register__form-group">
      <label class="register__form--label" for="password">Password:</label>
      <input
        class="register__form--input"
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
    <div class="register__form-group">
      <label class="register__form--label" for="password"
        >Confirm Password:</label
      >
      <input
        class="register__form--input"
        :class="!validator.passwordConfirm.valid ? 'invalid-input' : ''"
        type="password"
        v-model="user.passwordConfirm"
        placeholder="Confirm your password"
        @blur="validatePasswordConfirm"
        @input="validatePasswordConfirm"
      />
      <span v-if="!validator.passwordConfirm.valid" class="invalid-message">{{
        validator.passwordConfirm.message
      }}</span>
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
      validator: {
        userName: {
          message: '',
          valid: true,
        },
        name: {
          message: '',
          valid: true,
        },
        password: {
          message: '',
          valid: true,
        },
        passwordConfirm: {
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
    signUp() {
      if (this.validate()) {
        // sign up
      }
    },
    toSignIn() {
      this.$router.push({ name: 'login' })
    },
    validate() {
      let valid = true
      if (!this.validateUsername())
        valid = false
      if (!this.validateName())
        valid = false
      if (!this.validatePassword())
        valid = false
      if (!this.validatePasswordConfirm())
        valid = false
      return valid
    },
    validateUsername() {
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
    validateName() {
      let valid = true
      if (!this.user.name || this.user.name.length == 0) {
        this.validator.name.valid = false
        this.validator.name.message = 'Invalid name'
        valid = false
      }
      else {
        this.validator.name.valid = true
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
    },
    validatePasswordConfirm() {
      let valid = true
      if (!this.user.passwordConfirm || this.user.passwordConfirm.length == 0) {
        this.validator.passwordConfirm.valid = false
        this.validator.passwordConfirm.message = 'Invalid password confirmation'
        valid = false
      }
      else if (this.user.password != this.user.passwordConfirm) {
        this.validator.passwordConfirm.valid = false
        this.validator.passwordConfirm.message = 'Passwords do not match'
      }
      else {
        this.validator.passwordConfirm.valid = true
      }

      return valid
    },
  },
}
</script>
