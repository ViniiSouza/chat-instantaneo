<template>
  <FormComponent>
        <div class="login__form-group">
          <label class="login__form--label" for="username">Username:</label>
          <input
            class="login__form--input"
            type="text"
            id="username"
            v-model="username"
            placeholder="Enter your username"
          />
        </div>
        <div class="login__form-group">
          <label class="login__form--label" for="password">Password:</label>
          <input
            class="login__form--input"
            type="password"
            id="password"
            v-model="password"
            placeholder="Enter your password"
          />
        </div>
        <button id="login__form--sign-in" @click="signIn">Sign in</button>
        <p>
          Don't have an account?
          <a href="#" class="login__form--sign-up">Sign up</a>
        </p>
  </FormComponent>
</template>


<script>
import FormComponent from '../shared/form-section/FormComponent.vue'
import './shared/style.css'

export default {
  data() {
    return {}
  },
  components: {
    FormComponent,
  },
  methods: {
    signIn() {
      //
    }
  }
}
</script>