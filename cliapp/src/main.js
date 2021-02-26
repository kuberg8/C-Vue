import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import Vuelidate from 'vuelidate'

import Form from './components/Form'
import Contacts from './components/Contacts'

Vue.config.productionTip = false
Vue.use(Vuelidate)
Vue.use(VueRouter)


let router = new VueRouter({
    routes: [
        {
            path: '/',
            name: 'Form',
            component: Form
        },
        {
            path: '/Form',
            name: 'Form',
            component: Form
        },
        {
            path: '/Contacts',
            name: 'Contacts',
            component: Contacts
        }        
    ]
})


new Vue({
  render: h => h(App),
  router: router
}).$mount('#app')
