import ChooseComponent from './ChooseComponent/ChooseComponent.tsx';
import videoCardIcon from 'assets/videocard-white-icon.png';
import processorIcon from 'assets/cpu-white-icon.png';

function ChooseComponents() {
  return (
    <section>
      <ChooseComponent
        img={videoCardIcon}
        type={'videoCard'}
        title={'Видеокарта'}
        isImportant={true}
        errorType={'Success'}
      />
      <ChooseComponent
        img={processorIcon}
        type={'processor'}
        title={'Процессор'}
        isImportant={true}
        errorType={'Warning'}
      />
      <ChooseComponent
        img={videoCardIcon}
        type={'videoCard'}
        title={'Видеокарта'}
        isImportant={false}
        errorType={'Error'}
      />
    </section>
  );
}

export default ChooseComponents;
