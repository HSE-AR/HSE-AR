<template>
  <form class="form" @submit.prevent="handleSubmit">
    <div class="heading__container">
      <h1 class="heading__item">Sign up</h1>
      <div class="heading__item">
        <h2 class="heading__link">
          <a href="#">if you are new around here</a>
        </h2>
      </div>
    </div>
    <span class="form__input">
      <input v-model="email" id="email" type="text" placeholder="Email..." required>
    </span>

    <span class="form__input">
      <input v-model="password" id="password" type="password" placeholder="Password..." required>
    </span>

    <span class="form__input">
      <input v-model="name" id="name" type="text" placeholder="Name..." required>
    </span>

    <div class="sign">
      <button class="sign__button" type="submit">
        <span>Sign up</span>
      </button>
    </div>
  </form>
</template>

<script>
  import axios from 'axios'
  import router from '../../router/router'


  export default {
    name: 'SignUpAdmin',
    data() {
      return {
          email: "",
          password: "",
          name: "",
      }
    },
    methods:{
      handleSubmit() {
        const data = {
          'email': this.email,
          'password': this.password,
          'name': this.name,
        }
        axios.post('auth/register', data)
          .then(response => console.log(response))
          .catch(err => console.log(err))
        router.push('/adminka')
      }
    }
  }
</script>

<style lang="scss" scoped>
  @import 'SignAdminForm.scss';
</style>

