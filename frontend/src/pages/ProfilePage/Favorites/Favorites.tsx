import ChooseFavoritesCard from 'components/cards/ChooseFavoritesCard/ChooseFavoritesCard.tsx';
import VideoCard from 'assets/videocard.jpg';
import styles from './Favorites.module.css';

function Favorites() {
  return (
    <div className={styles.block}>
      <ChooseFavoritesCard
        className={styles.card}
        name={'Залупа моржа'}
        img={VideoCard}
        text={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        price={100}
      ></ChooseFavoritesCard>
      <ChooseFavoritesCard
        className={styles.card}
        name={'Залупа моржа'}
        img={VideoCard}
        text={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        price={100}
      ></ChooseFavoritesCard>
    </div>
  );
}

export default Favorites;
