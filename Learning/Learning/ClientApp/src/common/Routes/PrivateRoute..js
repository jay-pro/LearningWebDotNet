import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import {getCurrentUser, getCurrentRole} from '../Service/AuthService';

export const PrivateRoute = ({ component: Component, roles, ...rest }) => (
    <Route {...rest} render={props => {
        const currentUser = getCurrentUser();
        const currentRole = getCurrentRole();
        if (!currentUser) {
            // not logged in so redirect to login page with the return url
            return <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
        }
        // check if route is restricted by role
        if (roles && roles.indexOf(currentRole) === -1) {
            // role not authorised so redirect to home page
            return <Redirect to={{ pathname: '/'}} />
        }

        // authorised so return component
        return <Component {...props} />
    }} />
)