import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import HomePage from '../feature/activities/home/HomePage';

function App() {
	const location = useLocation();
	return (
		<>
			{location.pathname === '/' ? (
				<HomePage />
			) : (
				<>
					<NavBar />
					<Container style={{ marginTop: '7em' }}>
						<Outlet />
					</Container>
				</>
			)}
		</>
	);
}

// eslint-disable-next-line react-refresh/only-export-components
export default observer(App);
