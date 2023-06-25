<template>
  <div id="login__container">
    <!-- <img src="https://images.vexels.com/media/users/3/139911/isolated/preview/1afb4038427b2bd8edd275940aea269d-icone-de-servico-de-bate-papo.png" alt="Imagem" class="login__speech-icon--first"> -->
    <div id="login__card">
      <div id="login__logo--container">
        <img
          id="login__logo--image"
          src="https://i.imgur.com/zBJnlSa.png"
          alt="Logo do aplicativo"
        />
      </div>
      <form>
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
        <button id="login__form--sign-in" type="submit">Sign in</button>
        <p>
          Don't have an account?
          <a href="#" class="login__form--sign-up">Sign up</a>
        </p>
      </form>
    </div>
    <!-- <img src="https://images.vexels.com/media/users/3/139911/isolated/preview/1afb4038427b2bd8edd275940aea269d-icone-de-servico-de-bate-papo.png" alt="Imagem" class="login__speech-icon--second"> -->
  </div>
</template>


<script>
export default {
  data() {
    return {}
  },
}
</script>

<style scoped>
#login__container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: rgb(255, 210, 175);
  background: linear-gradient(
    90deg,
    rgba(255, 210, 175, 1) 0%,
    rgba(255, 141, 65, 1) 100%
  );
  z-index: 0;
}

#login__card {
  width: 300px;
  padding: 20px;
  background-color: #fff;
  border-radius: 5px;
  box-shadow: 0 10px 15px rgba(0, 0, 0, 0.2);
  z-index: 5;
}

#login__logo--container {
  text-align: center;
  margin-bottom: 20px;
}

#login__logo--container #login__logo--image {
  max-width: 100px;
}

.login__form-group {
  margin-bottom: 15px;
}

.login__form--label {
  display: block;
  font-weight: bold;
}

.login__form--input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

#login__form--sign-in {
  width: 100%;
  padding: 10px;
  background-color: #f07c3a;
  border: none;
  color: #fff;
  font-weight: bold;
  border-radius: 4px;
  cursor: pointer;
}

#login__form--sign-in:hover {
  background-color: #c05e26;
}

p {
  text-align: center;
}

.login__form--sign-up {
  color: #f07c3a;
  text-decoration: none;
}

.login__form--sign-up:hover {
  text-decoration: underline;
}

.login__speech-icon--first {
  position: relative;
  top: -130px;
  left: 25px;
  width: 100px;
  object-fit: cover;
  object-position: center right;
  z-index: -1;
}

.login__speech-icon--second {
  width: 100px;
  bottom: -130px;
  left: -15px;
  object-fit: cover;
  object-position: center right;
  z-index: -1;
}
</style>
