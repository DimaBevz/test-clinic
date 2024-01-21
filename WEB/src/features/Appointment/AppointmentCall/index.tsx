import { DyteMeeting } from '@dytesdk/react-ui-kit';
import { useDyteClient } from '@dytesdk/react-web-core';
import useAppointmentInfoController from '../AppointmentInfo/appointment-info.controller';
import { useEffect } from 'react';
import { useGetCallTokenMutation } from './appointment-call.service';

export const AppointmentCall = () => {
  const { getSessionInfo } = useAppointmentInfoController();
  const { data } = getSessionInfo;
  const [meeting, initMeeting] = useDyteClient();

  const [getCallToken,
  {
    data: getCallTokenData
  }] = useGetCallTokenMutation();

  useEffect(()=>{
    if(data?.id){
      getCallToken("bbb1b326-4dba-4b83-a44e-8065c43ab22a"??data?.id);
    }
  },[data?.id])

  useEffect(() => {
    if(getCallTokenData){
      initMeeting({
        authToken: getCallTokenData,
      });
    }
  }, [getCallTokenData]);

  return <DyteMeeting meeting={meeting!} onEnded={()=> console.log("ended")} />;
};
