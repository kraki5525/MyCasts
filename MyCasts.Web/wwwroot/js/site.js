import Vue from 'vue';
import App from './App'

var app = new Vue({
    el: '#app',
    components: { App },
    template: '<App/>'
    // data: {
    //     podcasts:[],
    //     name: "",
    //     url: ""
    // },
    // methods: {
    //     add: function(event) {
    //         this.podcasts.push({name: this.name, url: this.url});
    //         this.name = "";
    //         this.url = "";
    //     }
    // }
});