<template>
	<v-container grid-list-md py-0>
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
		}
	},
	created() {
		this.getAllOutlet();
	},
	computed: {
        ...mapState({
            outlets: state => state.outlet.outlets
        }),
    },
	methods: {
		searchOutlet(){
			console.log('search outlet')
			var name = this.outletName
			this.$store.dispatch('outlet/getOutletByName', name)
		},
		getAllOutlet(){
			this.$store.dispatch('outlet/getAllOutlet')
		},
		viewOutletDetail(id){
			console.log('view outlet ' + id)
			this.$router.push('/outlet')
			this.$store.dispatch('outlet/getOutletByID', id)
			var o_service = {
				idOutlet: id,
				pageSize: 4,
				pageNumber: 1
			}
			this.$store.dispatch('service/getServicesByOutlet',o_service)
			var o_therapist = {
				outletID: id,
				pageSize: 3,
				pageNumber: 1
			}
			this.$store.dispatch('therapist/getTherapistsByOutlet',o_therapist)
			// Chưa có phâng trang
			this.$store.dispatch('outlet/getOutletReview', id)
		}
	}
    }
</script>
