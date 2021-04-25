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
        Sign up
      </button>
    </div>

    <div style="text-align: center; padding: 10px">
      <router-link to="/signin/admin">Sign In</router-link>
    </div>


  </form>

  </div>
</template>

<script>
  import Swal from 'sweetalert2'

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
        this.$Progress.start()
        const data = {
          email: this.email,
          password: this.password,
          name: this.name,
        }
        this.$store.dispatch('registerUser', data)
          .then(() => {
              this.$Progress.finish()
              this.$router.push('/admin/profile')
          })
          .catch(err => {
              console.log(err)
              this.$Progress.fail()
              Swal.fire(
                  'Error',
                  'Login and password are not correct',
                  'error'
              )
          })

      }
    }
  }
</script>

<style lang="scss" scoped>
  @import 'SignAdminForm.scss';
</style>

