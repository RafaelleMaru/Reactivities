import { Card, Icon, Image } from 'semantic-ui-react';
import { Profile } from '../../app/models/profile';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react-lite';

interface Props {
	profile: Profile;
}

// eslint-disable-next-line react-refresh/only-export-components
export default observer(function ProfileCard({ profile }: Props) {
	return (
		<Card as={Link} to={`/profile/${profile.username}`}>
			<Image src={profile.image || '/assets/user.png'} />
			<Card.Content>
				<Card.Header>{profile.displayName}</Card.Header>
				<Card.Description>Bio goes here</Card.Description>
			</Card.Content>

			<Card.Content extra>
				<Icon name='user' />
				20 followers
			</Card.Content>
		</Card>
	);
});
