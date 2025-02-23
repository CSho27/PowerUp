import { useMemo } from "react";
import { useAppContext } from "../app/appContext";
import { InitializeGameSaveManagerApiClient } from "../app/gameSaveManager/initializeGameSaveManagerApiClient";
import styled from "styled-components";
import { FONT_SIZES } from "../style/constants";
import { Icon } from "../components/icon/icon";
import { openGameSaveManagerInitializationModal } from "../app/gameSaveManager/gameSaveManagerInitializationModal";
import { openGameSaveManagerModal } from "../app/gameSaveManager/gameSaveManagementModal";
import { openMigrationModal } from "../app/migrationModal/migrationModal";

export 

function ElectronToolbarActions() {
  const appContext = useAppContext();
  const initializeGSManager = useMemo(
   () => new InitializeGameSaveManagerApiClient(appContext.commandFetcher),
   []
  );
  
  return <>
    <HeaderLinkWrapper onClick={openGameSaveManagementModal} title='Open Game Save Manager'>
      Game Saves
      <Icon icon='sd-card' />
    </HeaderLinkWrapper>
    <HeaderLinkWrapper onClick={importOldData} title='Import data from an older version of the PowerUp app'>
      Import PowerUp Data
      <Icon icon='upload' />
    </HeaderLinkWrapper>
  </>

  async function openGameSaveManagementModal() {
    const initializationResponse = await initializeGSManager.execute({});
    if(!initializationResponse.success) {
      const shouldOpenManager = await openGameSaveManagerInitializationModal(appContext);
      if(!shouldOpenManager)
        return;
    }
    openGameSaveManagerModal(appContext);
  }

  function importOldData() {
    openMigrationModal(appContext);
  }
}

const HeaderLinkWrapper = styled.a`    
  font-size: ${FONT_SIZES.default_16};
  color: white;
  display: flex;
  justify-content: flex-end;
  align-items: baseline;
  gap: 4px;
  text-decoration: none;
  cursor: pointer;
`