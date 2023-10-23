import { createApp } from 'vue'
import App from '@/App.vue'
import router from '@/router/router.js'
import '@/assets/css/index.scss'
import PrimeVue from 'primevue/config';

//primevue components
import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Avatar from 'primevue/avatar'
import InputNumber from 'primevue/inputnumber'
import Card from 'primevue/card'
import Chip from 'primevue/chip'
import InlineMessage from 'primevue/inlinemessage'


const app = createApp(App)

app.component('Dialog', Dialog)
app.component('Button', Button)
app.component('InputText', InputText)
app.component('Avatar', Avatar)
app.component('InputNumber', InputNumber)
app.component('Card', Card)
app.component('Chip', Chip)
app.component('InlineMessage', InlineMessage)

app.use(router)
app.use(PrimeVue);
app.mount('#app')