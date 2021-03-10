<template>
  <div>
  <form class="form" @submit.prevent="signUp">
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
      <button @submit.prevent="signUp" class="sign__button" type="submit">
        <span>Sign up</span>
      </button>
    </div>
    <router-link to="/signin/admin">Sign In</router-link>

  </form>

  </div>
</template>

<script>
  import axios from 'axios'

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
      signUp() {
        const data = {
          email: this.email,
          password: this.password,
          name: this.name,
        }
        this.$store.dispatch('registerUser', data)
          .then(() => {
              this.$router.push('/admin/profile')
          })
          .catch(err => {
              console.log(err)
          })

      }
    }
  }
</script>

<style lang="scss" scoped>
  @import 'SignAdminForm.scss';
</style>

