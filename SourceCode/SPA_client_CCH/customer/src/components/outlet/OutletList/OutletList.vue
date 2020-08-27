<template>
	<v-container grid-list-md py-0>
		 <v-layout row wrap v-if="noti != null">
			<app-alert
				:noti="noti"
				@dismissed="onDismissed"
				>
			</app-alert>
		</v-layout>
		<v-layout justify-center pt-4>
			<v-flex xs13 sm8>
				<v-text-field
					hide-details
					v-model="outletName"
					label="Outlet Search By Name"
					outline
					append-icon="search"
					@keyup.enter="searchOutlet()"
					@click:append="searchOutlet()"
				></v-text-field>
			</v-flex>
		</v-layout>
		 <v-layout
            v-if="loading"
            column fill-height align-center justify-center px-2 pt-0>
            <v-progress-linear
                :indeterminate="loading"
                height="5"
            >              
            </v-progress-linear>
        </v-layout>
		<v-layout
			justify-center
			v-for="outlet in outlets" :key='outlet.index'
			
			>
			<v-flex xs12 sm8  class="text-xs-center">
      			<v-card>
        			<v-img
						@mousedown="viewOutletDetail(outlet.id)"
						height="200"
				        src="https://www.google.com/url?sa=i&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwjwxIOS293fAhWEZt4KHfk7C18QjRx6BAgBEAU&url=https%3A%2F%2Fdaotaoquanlyspa.com%2Fnhung-luu-y-khi-mo-spa%2F&psig=AOvVaw0EP3T2vf-yYfgN0K9h8W3W&ust=1547020640943931"
				      
        			></v-img>
      				<v-card-actions class="px-3 py-2">
						<h4 class="mb-0">{{outlet.name}}</h4>
						<v-spacer></v-spacer>
						<v-rating
							v-model="outlet.star"
							half-increments
							color="yellow darken-3"
          					background-color="grey darken-1"
							empty-icon="$vuetify.icons.ratingFull">
						</v-rating>
						<span></span>
						<span>{{ outlet.review }} reviews</span>
					</v-card-actions>
      			</v-card>
    		</v-flex>
		</v-layout>
	</v-container>
</template>

<script >
import { mapState, mapActions } from 'vuex'
export default {
	data () {
		return{
			outletName:'',
			noti: null
		}
	},
	created() {
		this.getAllOutlet();
	},
	computed: {
        ...mapState({
			outlets: state => state.outlet.outlets,
			loading: state => state.share.loading,
			noti: state => state.share.noti,

		}),
    },
	methods: {
		searchOutlet(){
			console.log('search outlet')
			var name = this.outletName
			if(name == '') {
				console.log('search null')
				this.noti={
					type: 'warning',
					mes: 'Outlet name can not empty !!!'
				}
				 commit('share/setNoti', {payload: noti} , { root: true })
        		commit('share/setLoading', {payload: false} , { root: true })

			}
			else
			{
				this.$store.dispatch('outlet/getOutletByName', name)
				this.noti =null
			}
		},
		getAllOutlet(){
			this.$store.dispatch('outlet/getAllOutlet')
		},
		viewOutletDetail(id){
			console.log('view outlet ' + id)
			this.$store.dispatch('outlet/changeIDCurrentOutLet',id)
			this.$router.push('/outlet')
			
		},
		onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }
	}
    }
</script>
