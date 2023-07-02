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
    <div class="register__form-group">
      <label>Public profile&nbsp;</label>
      <Tooltip>
        <template v-slot:component>
           <font-awesome-icon icon="fa-regular fa-circle-question" />
        </template>
        <template v-slot:content>
          <p style="font-weight: 500; margin: 0;">When this option is on, any user can send you messages. If not, they will have to send you a request.</p>
        </template>
      </Tooltip>
      <ToogleButton style="position: absolute; right: 0; display: absolute" v-model="user.isPublicProfile" :value="user.isPublicProfile" @setCheckboxVal="setPublicProfileVal"/>
    </div>
    <button id="register__form--sign-in" @click="signUp">Sign up</button>
    <p>
      Already have an account?
      <a href="#" class="register__form--sign-up" @click="toSignIn">Sign in</a>
    </p>
  </FormComponent>
</template>
<script setup>
import './shared/style.css'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import FormComponent from '../shared/form-section/FormComponent.vue'
import ToogleButton from '../shared/toogle-button/ToggleButton.vue'
import Tooltip from '../shared/tooltip/Tooltip.vue'
import api from './shared/api.js'
import { useToast } from 'vue-toastification'

const toast = useToast()
const router = useRouter()

const user = ref({
  userName: '',
  name: '',
  password: '',
  passwordConfirm: '',
  isPublicProfile: true
})

const validator = ref({
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
})

const validateUsername = () => {
  let valid = true
  if (!user.value.userName || user.value.userName.length == 0) {
    validator.value.userName.valid = false
    validator.value.userName.message = 'Invalid username'
    valid = false
  } else {
    validator.value.userName.valid = true
    validator.value.userName.message = ''
  }

  return valid
}

const validateName = () => {
  let valid = true
  if (!user.value.name || user.value.name.length == 0) {
    validator.value.name.valid = false
    validator.value.name.message = 'Invalid name'
    valid = false
  } else {
    validator.value.name.valid = true
    validator.value.name.message = ''
  }

  return valid
}

const validatePassword = () => {
  let valid = true
  if (!user.value.password || user.value.password.length == 0) {
    validator.value.password.valid = false
    validator.value.password.message = 'Invalid password'
    valid = false
  } else {
    validator.value.password.valid = true
    validator.value.password.message = ''
  }

  return valid
}

const validatePasswordConfirm = () => {
  let valid = true
  if (!user.value.passwordConfirm || user.value.passwordConfirm.length == 0) {
    validator.value.passwordConfirm.valid = false
    validator.value.passwordConfirm.message = 'Invalid passwordConfirm'
    valid = false
  } else if (user.value.password != user.value.passwordConfirm) {
    validator.value.passwordConfirm.valid = false
    validator.value.passwordConfirm.message = 'Passwords do not match'
  } else {
    validator.value.passwordConfirm.valid = true
    validator.value.passwordConfirm.message = ''
  }

  return valid
}

const validate = () => {
  let valid = true
  if (!validateUsername()) valid = false
  if (!validateName()) valid = false
  if (!validatePassword()) valid = false
  if (!validatePasswordConfirm()) valid = false
  return valid
}

const signUp = () => {
  if (validate()) {
    api
      .signUp(user.value)
      .then((response) => {
        if (response.status == 201) {
          localStorage.setItem('token', response.data)
          router.push({ name: 'chat' })
          toast
        }
      })
      .catch((err) => {
        if (err.response && err.response.data) {
          toast.error(err.response.data)
          validator.value.passwordConfirm.message = err.response.data
        }
        else {
          const errorMsg = 'Something went wrong. Try again later.'
          toast.error(errorMsg)
          validator.value.passwordConfirm.message = errorMsg
        }
        validator.value.userName.valid = false
        validator.value.name.valid = false
        validator.value.password.valid = false
        validator.value.passwordConfirm.valid = false
      })
  }
}

const toSignIn = () => router.push({ name: 'login' })
</script>
