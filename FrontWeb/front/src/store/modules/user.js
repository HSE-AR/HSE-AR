export default {
    state: {
        user:null,
    },
    getters: {
        user: state => {
            return state.user
        }
    },
    actions: {
        getUser(context, user) {
            context.commit('setUser', user)
        }
    },
    mutations: {
        setUser(state,user) {
            state.user = user
        }
    }
}