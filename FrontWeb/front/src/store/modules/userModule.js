import axios from "axios";


export default {
    state: {
        status: '',
        token: localStorage.getItem('token') || null,
        user: null,
        company_actions: localStorage.getItem('company_actions') || []
    },

    getters: {
        isLoggedIn: state => !!state.token,
        authStatus: state => state.status,
        user: state => state.user,
        token: state => state.token,
        company_actions: state => state.company_actions

    },

    actions: {

        getTokenAndLogin({ commit }, user) {
            return new Promise((resolve, reject) => {
                commit('auth_request')
                axios.post('https://localhost:5555/wapi/auth/login', user)
                    .then(response => {
                        const token = response.data
                        localStorage.setItem('token', token)
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        commit('get_token_success', token)
                        resolve(response)
                    })
                    .catch(err => {
                        commit('auth_error')
                        localStorage.removeItem('token')
                        reject(err)
                    })
            })


        },

        getUserFromToken(context) {
            return new Promise((resolve, reject) => {
                axios.get('https://localhost:5555/wapi/account')
                    .then(response => {
                        const user = response.data
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        context.commit('get_user_success', user)
                        resolve(response)
                    })
                    .catch(err => {
                        reject(err)
                    })
            })

        },

        getCompanyFromToken(context) {
            return new Promise((resolve, reject) => {
                axios.get('https://localhost:5555/wapi/company')
                    .then(response => {
                        const companyActions = response.data
                        const token = context.getters.token
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        localStorage.setItem('company_actions', JSON.stringify(companyActions))
                        context.commit('get_company_actions_success', companyActions)
                        resolve(response)
                    })
                    .catch(err => {
                        console.log(err)
                        reject(err)
                    })
            })
        },

        registerUser({ commit }, user) {
            return new Promise((resolve, reject) => {
                commit('auth_request')
                axios.post('https://localhost:5555/wapi/auth/register', user)
                    .then(response => {
                        const token = response.data
                        localStorage.setItem('token', token)
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token
                        commit('get_token_success', token)
                        resolve(response)
                    })
                    .catch(err => {
                        commit('auth_error')
                        localStorage.removeItem('token')
                        reject(err)
                    })
            })
        },



        logOut({ commit }) {
            return new Promise((resolve, reject) => {
                commit('logout')
                localStorage.removeItem('token')
                localStorage.removeItem('company_actions')
                delete axios.defaults.headers.common['Authorization']
                resolve()
            })
        },





    },
    mutations: {
        auth_request(state){
            state.status = 'loading'
        },
        get_token_success(state, token) {
            state.token = token
            state.status = 'success'
        },
        get_user_success(state, user) {
            state.user = user
            state.status = 'success'
        },
        get_company_actions_success(state, company_actions) {
            state.company_actions = company_actions
            state.status = 'success'
        },
        auth_error(state){
            state.status = 'error'
        },
        logout(state){
            state.status = ''
            state.token = null
        },
    },
}