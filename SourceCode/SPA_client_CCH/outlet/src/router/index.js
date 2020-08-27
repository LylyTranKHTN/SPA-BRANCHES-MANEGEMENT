// Đây là thư mục cấu hình đường dẫn url của màn hình

import Vue from 'vue'
import Router from 'vue-router'

import DashBoard from '@/components/Container/DashBoard'

// Bed

import AddBed from '@/components/Bed/AddBed'
import EditBed from '@/components/Bed/EditBed'
import BedList from '@/components/Bed/BedList'

// Room
import AddRoom from '@/components/Room/AddRoom'
import EditRoom from '@/components/Room/EditRoom'
import RoomList from '@/components/Room/RoomList'

// Outlet
import OutletList from '@/components/Outlet/OutletList'
import AddOutlet from '@/components/Outlet/AddOutlet'
import EditOutlet from '@/components/Outlet/EditOutlet'

// Service
import ServiceList from '@/components/Service/ServiceList'
import AddService from '@/components/Service/AddService'
import EditService from '@/components/Service/EditService'

// User
import UserList from '@/components/User/UserList'
import EditUser from '@/components/User/EditUser'
import AddUser from '@/components/User/AddUser'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: DashBoard,
      meta: {
        draw: true,
      },
    },
    {
      path: '/beds',
      name: 'beds',
      component: BedList,
      meta: {
        draw: true,
      },
    },
    {
      path: '/bed/add',
      name: 'add new bed',
      component: AddBed,
      meta: {
        draw: false,
        to: '/beds'
      },
    },
    {
      path: '/rooms',
      name: 'rooms',
      component: RoomList,
      meta: {
        draw: true,
      },
    },
    {
      path: '/room/add',
      name: 'add new room',
      component: AddRoom,
      meta: {
        draw: false,
        to: '/rooms'
      },
    },
    {
      path: '/room/edit',
      name: 'edit room',
      component: EditRoom
    },
    {
      path: '/outlets',
      name: 'outlets',
      component: OutletList,
      meta: {
        draw: true,
      },
    },
    {
      path: '/outlet/add',
      name: 'add new outlet',
      component: AddOutlet,
      meta: {
        draw: false,
        to: '/rooms'
      },
    },
    {
      path: '/outlet/edit',
      name: 'edit outlet',
      component: EditOutlet
    },
    {
      path: '/services',
      name: 'services',
      component: ServiceList,
      meta: {
        draw: true,
      },
    },
    {
      path: '/service/add',
      name: 'add new service',
      component: AddService,
      meta: {
        draw: false,
        to: '/rooms'
      },
    },
    {
      path: '/service/edit',
      name: 'edit service',
      component: EditService
    },
    {
      path: '/users',
      name: 'users',
      component: UserList,
      meta: {
        draw: true,
      },
    },
    {
      path: '/user/add',
      name: 'add new user',
      component: AddUser,
      meta: {
        draw: false,
        to: '/rooms'
      },
    },
    {
      path: '/user/edit',
      name: 'edit user',
      component: EditUser
    }
  ]
})
