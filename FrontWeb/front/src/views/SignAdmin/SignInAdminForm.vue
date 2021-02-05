<template>
  <form class="form" @submit.prevent="handleSubmit">
    <div class="heading__container">
      <h1 class="heading__item">Sign In</h1>
      <div class="heading__item">
        <h2 class="heading__link">
          <a href="#">or use your account</a>
        </h2>
      </div>
    </div>
    <span class="form__input">
      <input v-model="email" type="text" id="email" class="email-input" placeholder="Email..." required>
    </span>

    <span class="form__input">
      <input v-model="password" type="password" id="password" class="password-input" placeholder="Password..." required>
    </span>
    <div class="sign">
      <button class="sign__button" type="submit">
        <span>Sign in</span>
      </button>
    </div>
<!--          <button @click="signUpAdmin()">Регистрация</button>-->
  </form>

</template>

<script>
  import axios from 'axios'
  export default {
    name: 'SignInAdmin',
    data() {
      return {
        email: '',
        password: '',
      }
    },
    methods: {
      async handleSubmit() {
        const response = await axios.post('Auth/Login', {
          "email": this.email,
          "password": this.password,
        })

        localStorage.setItem('token', response.data.token)
        await this.$store.dispatch('user', response.data.user)
        await this.$router.push('adminka')
      },

      signUpAdmin() {
          this.$router.push('/signup/admin')
      }
    }

  }
</script>

<style lang="scss" scoped>
  @import 'SignAdminForm.scss';
</style>
