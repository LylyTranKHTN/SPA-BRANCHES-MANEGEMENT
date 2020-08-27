<template>
  <v-alert
    outline
    transition="scale-transition"
    :type='noti.type'
    dismissible
    @input="onClose"
    :value="true">
    {{ noti.mes }}
  </v-alert>
</template>

<script>
  export default {
    props: ['noti'],
    methods: {
      onClose () {
        this.$emit('dismissed')
      }
    }
  }
</script>