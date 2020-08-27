<template>
  <v-container>
    <v-layout>
      <!-- Choose Outlet -->
      <v-layout pb-2>
        <v-select
          size="5"
          label="Choose Oulet"
          outline
          :items="outlets"
          v-model="currentOutlet"
          item-text="name"
          item-value="id"
          hide-details
          @mousedown="loadOutLets"
          @change="chooseOutlet(currentOutlet.id)"
        >
        </v-select>
      </v-layout>
      <!-- Button Add -->
        <v-btn fab dark color="green" small>
          <v-icon dark>add</v-icon>
        </v-btn>
    </v-layout>

    <!-- Search Customer -->
    <v-layout pb-2>
        <v-text-field
          label="Name, Phone, NRIC, Email"
          append-icon="search"
          outline
          v-model="txt"

          @click:append="searchCustomer(currentOutlet.id, txt)"
        >
        </v-text-field>
    </v-layout>

    <!-- List Customer -->
    <v-layout v-for="customer in customers" :key="customer.index">
      <v-flex xs10 sm7 md7>
        <v-card>
          <v-container grid-list-xs>
            <v-layout row wrap>
              <v-flex xs3>
                <v-avatar
                  size="65"
                  color="red"
                >
                  <v-img :src="customer.avatar" alt="alt">
                  </v-img>
                </v-avatar>
              </v-flex>
              <v-flex>
                <v-layout row column>
                  <v-flex pa-0>
                    <span>{{customer.name}}</span>
                    <br>
                    <span class="pr-4">{{customer.email}}</span>
                    <br>
                    <span>{{customer.phonenumber}} </span>
                  </v-flex>
                </v-layout>

                <v-layout>
                  <v-flex>
                    <v-btn outline color="indigo">Profile</v-btn>
                  </v-flex>

                  <v-flex>
                    <v-btn outline color="indigo">Booking List</v-btn>
                  </v-flex>
                </v-layout>
              </v-flex>
            </v-layout>
            <v-divider></v-divider>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>

  </v-container>
</template>
<script>
  import {mapState, mapActions} from 'vuex'

  export default {
    // props: {

    // },
    data() {
      return {
        outlet: '',
        txt: null,

      }
    },
    created() {
      this.loadOutLets()
      this.chooseOutlet(this.currentOutlet.id)
      // this.searchCustomer(this.currentOutlet.id,txt)
    },
    computed: {
      ...mapState({
        customers: state => state.customer.customers,
        outlets: state => state.outlet.outlets,

      }),
      currentOutlet: {
        get() {
          return this.$store.state.outlet.outlet
        },
        set(newID) {
          this.$store.dispatch('outlet/changeIDCurrentOutLet', newID)
        }
      },
    },
    // for function for event
    methods: {

      loadOutLets() {
        this.$store.dispatch('outlet/getAllOutlet')
      },
      chooseOutlet(outletID) {
        var o = {
          idOutlet: outletID
          // pageSize: 5,
          // pageNumber: 1
        }
        this.$store.dispatch('customer/GetAllCustomerByOutlet', o)
      },
      searchCustomer(outletID, customerInfo) {
        console.log('search customer')
        var cus = {
          outletID: outletID,
          txt: customerInfo
        }
        this.$store.dispatch('customer/GetCustomerByOutletAndText', cus)
      },

    }
  }
</script>
