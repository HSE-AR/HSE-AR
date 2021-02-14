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
  import router from '../../router/router'
  export default {
    name: 'SignInAdmin',
    data() {
      return {
        email: '',
        password: '',
      }
    },
    methods: {
      handleSubmit() {
        const data = {
          'email': this.email,
          'password': this.password,
        }
        axios.post('auth/login', data)
          .then(response => {
            console.log(response)
            localStorage.setItem('token', response.data)
            router.push('/adminka')
          })
          .catch(err => console.log(err))

        // localStorage.setItem('token', response.data.token)
        // this.$store.dispatch('user', response.data.user)

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
