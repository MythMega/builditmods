using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildItModsSelector
{
    public partial class Translation : Form
    {
        private string version = "1.1pre-release-1";
        private int app_build = 10;

        public Translation()
        {
            InitializeComponent();
        }

        public string getLanguageCode()
        {
            string ProfileFilePath = Path.Combine("sys", "profile.txt");
            string[] lines = File.ReadAllLines(ProfileFilePath);
            string code = "EN";

            foreach (string line in lines)
            {
                if (line.StartsWith("language="))
                {
                    string language = line.Substring(line.IndexOf('=') + 1);
                    Console.WriteLine($"Current language: {language}");
                    return language;
                }
            }
            return code;
        }

        private Dictionary<string, string> getContributor()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            return keyValuePairs;
        }

        private string getContributorsNamesAndLbl()
        {
            string res = string.Empty;
            return res;
        }

        public string translatedText(string textCode)
        {
            string resultat = "MISSING TRANSLATION";
            switch (getLanguageCode())
            {
                // french
                case "FR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Une erreur s'est produite :( attention ce logiciel doit être placé dans le dossier mods de votre .minecraft !\nVerifiez aussi que vous avez bien téléchargé les mods !"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifiez votre connexion."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Cette feature n'est pas encore implémentée"; break;
                        case "ERR_EXEC-BATCH": resultat = "Une erreur s'est produite lors de l'exécution du fichier batch : "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Il semblerait qu'il soit pas possible de lancer minecraft"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Vous n'avez selectionné aucun item, l'action a été ignoré"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Cette fonctionalité n'a pas encore été implementée."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Cette fonctionalité n'a pas encore été implementée. Cette feature sera disponible en version "; break;

                        case "WARN_WRONGFOLDER": resultat = "Mauvais dossier : "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Le logiciel doit être exécuté depuis le dossier 'mods' de votre .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Regarde ce que ça fait..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Ouvrir le rapport de problème."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Ouvre l'URL du changelog, résumé des changement par mises à jour."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Ouvre le repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Ouvre l'URL pour télécharger Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Télécharger / Mettre à jour les mods"; break;

                        case "INFO_SUCCESS": resultat = "Tout s'est déroulé avec succés !"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Je suis MythMega (j'ai pas encore crée cette section, c'est moins urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Les contributeurs sont les suivants :\n{getContributorsNamesAndLbl()}\nMerci à eux !"; break;

                        case "CONTRIBUTORS": resultat = "Contributeurs"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduction"; break;
                        case "CONTRIB_DEV": resultat = "Developpement"; break;
                        case "CONTRIB_ARTIST": resultat = "Artiste/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testeur"; break;
                        case "CONTRIB_OTHER": resultat = "Autre"; break;

                        case "LBL_WARNING": resultat = "Attention !"; break;
                        case "LBL_CONFIRMATION": resultat = "ëtes vous sur de vouloir faire cette action ?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tout sélectionner"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tout déselctionner"; break;
                        case "LBL_STOP": resultat = "Arrêter"; break;
                        case "LBL_OUI": resultat = "Oui"; break;
                        case "LBL_NON": resultat = "Non"; break;
                        case "LBL_ANNULER": resultat = "Annuler"; break;
                        case "LBL_LOCALISER": resultat = "Localiser"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosmétiques GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosmétiques Jeu"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosmétiques de Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Commandes Admin"; break;

                        case "LAYOUT_IsEnabled": resultat = "Activé"; break;

                        case "LAYOUT_EnableAll": resultat = "Tout Activer"; break;
                        case "LAYOUT_DisableAll": resultat = "Tout Désactiver"; break;
                        case "LAYOUT_Execute": resultat = "Exécuter"; break;
                        case "LAYOUT_InstallFabric": resultat = "Installer Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Notes de Mise à Jour"; break;
                        case "LAYOUT_GithubSource": resultat = "Code Source GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Ouvrir le Dossier des Mods"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Mettre à Jour/Télécharger les Mods"; break;
                        case "LAYOUT_Update": resultat = "Mettre à Jour"; break;
                        case "LAYOUT_Report": resultat = "Signaler"; break;
                        case "LAYOUT_Quit": resultat = "Quitter"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Démarrer Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Gestionnaire de Shaders"; break;
                        case "LAYOUT_RessourceManager": resultat = "Gestionnaire de Ressources"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Profils Rapides"; break;
                        case "LAYOUT_GBSystem": resultat = "Système"; break;
                        case "LAYOUT_GBAddOn": resultat = "Complément"; break;

                        case "LAYOUT_ProfilSave": resultat = "Sauvegarder le Profil"; break;
                        case "LAYOUT_File": resultat = "Fichier"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Actualiser les Profils"; break;
                        case "LAYOUT_ProfilReset": resultat = "Réinitialiser les Profils"; break;
                        case "LAYOUT_Setting": resultat = "Réglages"; break;
                        case "LAYOUT_About": resultat = "À Propos"; break;
                        case "LAYOUT_Theme": resultat = "Thème"; break;
                        case "LAYOUT_Contributors": resultat = "Contributeurs"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // English
                case "EN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "An error occurred :( Please ensure this software is placed in the 'mods' folder of your .minecraft!\nAlso, make sure you have downloaded the mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Check your connection."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "This feature is not yet implemented."; break;
                        case "ERR_EXEC-BATCH": resultat = "An error occurred while executing the batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "It seems that launching Minecraft is not possible."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "You haven't selected any items, the action was ignored."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "This feature has not been implemented yet."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "This feature has not been implemented yet. This feature will be available in version "; break;

                        case "WARN_WRONGFOLDER": resultat = "Wrong folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "The software must be executed from the 'mods' folder of your .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "See what it does..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Open problem report."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Open the changelog URL, summary of changes per updates."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Open the GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Open the URL to download Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Download / Update mods"; break;

                        case "INFO_SUCCESS": resultat = "Everything went successfully!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "I am MythMega (I haven't created this section yet, it's less urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"The contributors are as follows:\n{getContributorsNamesAndLbl()}\nThanks to them!"; break;

                        case "CONTRIBUTORS": resultat = "Contributors"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Translation"; break;
                        case "CONTRIB_DEV": resultat = "Development"; break;
                        case "CONTRIB_ARTIST": resultat = "Artist/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Other"; break;

                        case "LBL_WARNING": resultat = "Warning!"; break;
                        case "LBL_CONFIRMATION": resultat = "Are you sure you want to perform this action?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Select All"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Deselect All"; break;
                        case "LBL_STOP": resultat = "Stop"; break;
                        case "LBL_OUI": resultat = "Yes"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancel"; break;
                        case "LBL_LOCALISER": resultat = "Locate"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosmetic GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosmetic Game"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosmetic Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Admin Commands"; break;

                        case "LAYOUT_IsEnabled": resultat = "Is Enabled"; break;

                        case "LAYOUT_EnableAll": resultat = "Enable All"; break;
                        case "LAYOUT_DisableAll": resultat = "Disable All"; break;
                        case "LAYOUT_Execute": resultat = "Execute"; break;
                        case "LAYOUT_InstallFabric": resultat = "Install Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Update Notes"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub Source"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Open Mod Folder"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Update/Download Mods"; break;
                        case "LAYOUT_Update": resultat = "Update"; break;
                        case "LAYOUT_Report": resultat = "Report"; break;
                        case "LAYOUT_Quit": resultat = "Quit"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Start Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Shader Manager"; break;
                        case "LAYOUT_RessourceManager": resultat = "Resource Manager"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Fast Profiles"; break;
                        case "LAYOUT_GBSystem": resultat = "System"; break;
                        case "LAYOUT_GBAddOn": resultat = "Add-On"; break;

                        case "LAYOUT_ProfilSave": resultat = "Save Profile"; break;
                        case "LAYOUT_File": resultat = "File"; break;
                        case "LAYOUT_Profil": resultat = "Profile"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Refresh Profiles"; break;
                        case "LAYOUT_ProfilReset": resultat = "Reset Profiles"; break;
                        case "LAYOUT_Setting": resultat = "Settings"; break;
                        case "LAYOUT_About": resultat = "About"; break;
                        case "LAYOUT_Theme": resultat = "Theme"; break;
                        case "LAYOUT_Contributors": resultat = "Contributors"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Spanish
                case "ES":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ocurrió un error :( Asegúrate de que este software esté ubicado en la carpeta 'mods' de tu .minecraft.\n¡También asegúrate de haber descargado los mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Tiempo agotado - Verifica tu conexión."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta característica aún no está implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ocurrió un error al ejecutar el archivo por lotes: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que no es posible lanzar Minecraft."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "No has seleccionado ningún elemento, la acción fue ignorada."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Esta característica aún no ha sido implementada."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Esta característica aún no ha sido implementada. Esta función estará disponible en la versión "; break;

                        case "WARN_WRONGFOLDER": resultat = "Carpeta incorrecta: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "El software debe ejecutarse desde la carpeta 'mods' de tu .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Observa lo que hace..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir informe de problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir la URL del registro de cambios, resumen de cambios por actualizaciones."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir el repositorio de GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir la URL para descargar Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Descargar / Actualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "¡Todo se realizó exitosamente!"; break;
                        case "INFO_ABOUT": resultat = $"Mis Mods de Supervivencia\npor MythMega\n2023\nVersión: {version}\nCompilación: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Soy MythMega (aún no he creado esta sección, es menos urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Los contribuyentes son los siguientes:\n{getContributorsNamesAndLbl()}\n¡Gracias a ellos!"; break;

                        case "CONTRIBUTORS": resultat = "Contribuyentes"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traducción"; break;
                        case "CONTRIB_DEV": resultat = "Desarrollo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Diseño"; break;
                        case "CONTRIB_TESTER": resultat = "Probador"; break;
                        case "CONTRIB_OTHER": resultat = "Otro"; break;

                        case "LBL_WARNING": resultat = "¡Advertencia!"; break;
                        case "LBL_CONFIRMATION": resultat = "¿Estás seguro de que deseas realizar esta acción?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Seleccionar Todo"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Deseleccionar Todo"; break;
                        case "LBL_STOP": resultat = "Detener"; break;
                        case "LBL_OUI": resultat = "Sí"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosméticos GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosméticos de Juego"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosméticos de Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Comandos de Admin"; break;

                        case "LAYOUT_IsEnabled": resultat = "Habilitado"; break;

                        case "LAYOUT_EnableAll": resultat = "Habilitar Todo"; break;
                        case "LAYOUT_DisableAll": resultat = "Deshabilitar Todo"; break;
                        case "LAYOUT_Execute": resultat = "Ejecutar"; break;
                        case "LAYOUT_InstallFabric": resultat = "Instalar Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Notas de Actualización"; break;
                        case "LAYOUT_GithubSource": resultat = "Código Fuente en GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Abrir Carpeta de Mods"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Actualizar/Descargar Mods"; break;
                        case "LAYOUT_Update": resultat = "Actualizar"; break;
                        case "LAYOUT_Report": resultat = "Informe"; break;
                        case "LAYOUT_Quit": resultat = "Salir"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Iniciar Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Gestor de Shaders"; break;
                        case "LAYOUT_RessourceManager": resultat = "Gestor de Recursos"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Perfiles Rápidos"; break;
                        case "LAYOUT_GBSystem": resultat = "Sistema"; break;
                        case "LAYOUT_GBAddOn": resultat = "Complemento"; break;

                        case "LAYOUT_ProfilSave": resultat = "Guardar Perfil"; break;
                        case "LAYOUT_File": resultat = "Archivo"; break;
                        case "LAYOUT_Profil": resultat = "Perfil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Actualizar Perfiles"; break;
                        case "LAYOUT_ProfilReset": resultat = "Restablecer Perfiles"; break;
                        case "LAYOUT_Setting": resultat = "Configuración"; break;
                        case "LAYOUT_About": resultat = "Acerca de"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Contribuyentes"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // German
                case "DE":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ein Fehler ist aufgetreten :( Stelle sicher, dass diese Software im 'mods'-Ordner deines .minecraft-Verzeichnisses platziert ist!\nStelle außerdem sicher, dass du die Mods heruntergeladen hast!"; break;
                        case "ERR_TIMEOUT": resultat = "Zeitüberschreitung - Überprüfe deine Verbindung."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Diese Funktion ist noch nicht implementiert."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ein Fehler ist aufgetreten während der Ausführung der Stapeldatei: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Es scheint, dass das Starten von Minecraft nicht möglich ist."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Du hast keine Elemente ausgewählt, die Aktion wurde ignoriert."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Diese Funktion wurde noch nicht implementiert."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Diese Funktion wurde noch nicht implementiert. Diese Funktion wird in Version "; break;

                        case "WARN_WRONGFOLDER": resultat = "Falscher Ordner: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Die Software muss aus dem 'mods'-Ordner deines .minecraft-Verzeichnisses gestartet werden."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Schau, was es tut..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Problembericht öffnen."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Öffne die Changelog-URL, Zusammenfassung der Änderungen pro Update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Repository auf GitHub öffnen."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Öffne die URL zum Herunterladen von Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Mods herunterladen / aktualisieren"; break;

                        case "INFO_SUCCESS": resultat = "Alles verlief erfolgreich!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nvon MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ich bin MythMega (ich habe diesen Abschnitt noch nicht erstellt, es ist weniger dringend)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Die Beitragenden sind wie folgt:\n{getContributorsNamesAndLbl()}\nDanke an sie!"; break;

                        case "CONTRIBUTORS": resultat = "Mitwirkende"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Übersetzung"; break;
                        case "CONTRIB_DEV": resultat = "Entwicklung"; break;
                        case "CONTRIB_ARTIST": resultat = "Künstler/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Andere"; break;

                        case "LBL_WARNING": resultat = "Warnung!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bist du sicher, dass du diese Aktion durchführen möchtest?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Alle auswählen"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Alle abwählen"; break;
                        case "LBL_STOP": resultat = "Stop"; break;
                        case "LBL_OUI": resultat = "Ja"; break;
                        case "LBL_NON": resultat = "Nein"; break;
                        case "LBL_ANNULER": resultat = "Abbrechen"; break;
                        case "LBL_LOCALISER": resultat = "Lokalisieren"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Kosmetische GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Kosmetische Spiel"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Kosmetische Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Admin-Befehle"; break;

                        case "LAYOUT_IsEnabled": resultat = "Aktiviert"; break;

                        case "LAYOUT_EnableAll": resultat = "Alles aktivieren"; break;
                        case "LAYOUT_DisableAll": resultat = "Alles deaktivieren"; break;
                        case "LAYOUT_Execute": resultat = "Ausführen"; break;
                        case "LAYOUT_InstallFabric": resultat = "Fabric installieren"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Update-Notizen"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub-Quelle"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Mod-Ordner öffnen"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Mods aktualisieren / herunterladen"; break;
                        case "LAYOUT_Update": resultat = "Aktualisieren"; break;
                        case "LAYOUT_Report": resultat = "Melden"; break;
                        case "LAYOUT_Quit": resultat = "Beenden"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Minecraft starten"; break;
                        case "LAYOUT_ShaderManager": resultat = "Shader-Manager"; break;
                        case "LAYOUT_RessourceManager": resultat = "Ressourcen-Manager"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Schnelle Profile"; break;
                        case "LAYOUT_GBSystem": resultat = "System"; break;
                        case "LAYOUT_GBAddOn": resultat = "Add-On"; break;

                        case "LAYOUT_ProfilSave": resultat = "Profil speichern"; break;
                        case "LAYOUT_File": resultat = "Datei"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Profile aktualisieren"; break;
                        case "LAYOUT_ProfilReset": resultat = "Profile zurücksetzen"; break;
                        case "LAYOUT_Setting": resultat = "Einstellung"; break;
                        case "LAYOUT_About": resultat = "Über"; break;
                        case "LAYOUT_Theme": resultat = "Thema"; break;
                        case "LAYOUT_Contributors": resultat = "Mitwirkende"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Italian
                case "IT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Si è verificato un errore :( Assicurati che questo software sia posizionato nella cartella 'mods' della tua directory .minecraft!\nAssicurati anche di aver scaricato le mod!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifica la tua connessione."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Questa funzione non è ancora implementata."; break;
                        case "ERR_EXEC-BATCH": resultat = "Si è verificato un errore durante l'esecuzione del file batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sembra che avviare Minecraft non sia possibile."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Non hai selezionato alcun elemento, l'azione è stata ignorata."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Questa funzionalità non è ancora implementata."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Questa funzionalità non è ancora implementata. Questa funzione sarà disponibile nella versione "; break;

                        case "WARN_WRONGFOLDER": resultat = "Cartella errata: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Il software deve essere avviato dalla cartella 'mods' della tua directory .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Guarda cosa fa..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Apri il rapporto di problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Apri l'URL del registro delle modifiche, riassunto delle modifiche per aggiornamento."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Apri il repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Apri l'URL per scaricare Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Scarica / Aggiorna le mod"; break;

                        case "INFO_SUCCESS": resultat = "Tutto è andato bene!"; break;
                        case "INFO_ABOUT": resultat = $"I miei Mods di Sopravvivenza\ndi MythMega\n2023\nVersione: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Sono MythMega (non ho ancora creato questa sezione, è meno urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"I contributori sono i seguenti:\n{getContributorsNamesAndLbl()}\nGrazie a loro!"; break;

                        case "CONTRIBUTORS": resultat = "Contributori"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduzione"; break;
                        case "CONTRIB_DEV": resultat = "Sviluppo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Altro"; break;

                        case "LBL_WARNING": resultat = "Avviso!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sei sicuro di voler eseguire questa azione?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Seleziona tutto"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Deseleziona tutto"; break;
                        case "LBL_STOP": resultat = "Arresta"; break;
                        case "LBL_OUI": resultat = "Sì"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Annulla"; break;
                        case "LBL_LOCALISER": resultat = "Localizza"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosmetici GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosmetici di Gioco"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosmetici delle Skin"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Comandi Amministratore"; break;

                        case "LAYOUT_IsEnabled": resultat = "Abilitato"; break;

                        case "LAYOUT_EnableAll": resultat = "Abilita tutto"; break;
                        case "LAYOUT_DisableAll": resultat = "Disabilita tutto"; break;
                        case "LAYOUT_Execute": resultat = "Esegui"; break;
                        case "LAYOUT_InstallFabric": resultat = "Installa Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Note di Aggiornamento"; break;
                        case "LAYOUT_GithubSource": resultat = "Sorgente su GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Apri Cartella Mod"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Aggiorna/Scarica mod"; break;
                        case "LAYOUT_Update": resultat = "Aggiorna"; break;
                        case "LAYOUT_Report": resultat = "Segnala"; break;
                        case "LAYOUT_Quit": resultat = "Esci"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Avvia Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Gestore Shader"; break;
                        case "LAYOUT_RessourceManager": resultat = "Gestore Risorse"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Profili Veloci"; break;
                        case "LAYOUT_GBSystem": resultat = "Sistema"; break;
                        case "LAYOUT_GBAddOn": resultat = "Componente Aggiuntivo"; break;

                        case "LAYOUT_ProfilSave": resultat = "Salva Profilo"; break;
                        case "LAYOUT_File": resultat = "File"; break;
                        case "LAYOUT_Profil": resultat = "Profilo"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Aggiorna Profili"; break;
                        case "LAYOUT_ProfilReset": resultat = "Reimposta Profili"; break;
                        case "LAYOUT_Setting": resultat = "Impostazione"; break;
                        case "LAYOUT_About": resultat = "Informazioni"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Contributori"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Portuguese
                case "PT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ocorreu um erro :( Certifica-te de que este software está colocado na pasta 'mods' do teu diretório .minecraft!\nCertifica-te também de que fizeste o download das mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifica a tua ligação."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta funcionalidade ainda não foi implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ocorreu um erro ao executar o ficheiro em lote: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que não é possível iniciar o Minecraft."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Não selecionaste nenhum item, a ação foi ignorada."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Esta funcionalidade ainda não foi implementada."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Esta funcionalidade ainda não foi implementada. Esta funcionalidade estará disponível na versão "; break;

                        case "WARN_WRONGFOLDER": resultat = "Pasta errada: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "O software deve ser executado a partir da pasta 'mods' do teu diretório .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Vê o que faz..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir relatório de problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abre o URL do changelog, resumo das alterações por atualização."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir repositório GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abre o URL para descarregar o Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Descarregar / Atualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "Tudo correu bem!"; break;
                        case "INFO_ABOUT": resultat = $"Os meus Mods de Sobrevivência\nde MythMega\n2023\nVersão: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Eu sou o MythMega (ainda não criei esta secção, não é tão urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Os contribuidores são os seguintes:\n{getContributorsNamesAndLbl()}\nObrigado a eles!"; break;

                        case "CONTRIBUTORS": resultat = "Contribuidores"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Tradução"; break;
                        case "CONTRIB_DEV": resultat = "Desenvolvimento"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testador"; break;
                        case "CONTRIB_OTHER": resultat = "Outro"; break;

                        case "LBL_WARNING": resultat = "Aviso!"; break;
                        case "LBL_CONFIRMATION": resultat = "Tens a certeza de que queres realizar esta ação?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Selecionar Todos"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Deselecionar Todos"; break;
                        case "LBL_STOP": resultat = "Parar"; break;
                        case "LBL_OUI": resultat = "Sim"; break;
                        case "LBL_NON": resultat = "Não"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosméticos GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosméticos de Jogo"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosméticos de Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Comandos de Administração"; break;

                        case "LAYOUT_IsEnabled": resultat = "Ativado"; break;

                        case "LAYOUT_EnableAll": resultat = "Ativar Tudo"; break;
                        case "LAYOUT_DisableAll": resultat = "Desativar Tudo"; break;
                        case "LAYOUT_Execute": resultat = "Executar"; break;
                        case "LAYOUT_InstallFabric": resultat = "Instalar Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Notas de Atualização"; break;
                        case "LAYOUT_GithubSource": resultat = "Código Fonte no GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Abrir Pasta de Mods"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Atualizar/Descarregar mods"; break;
                        case "LAYOUT_Update": resultat = "Atualizar"; break;
                        case "LAYOUT_Report": resultat = "Reportar"; break;
                        case "LAYOUT_Quit": resultat = "Sair"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Iniciar Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Gestor de Shaders"; break;
                        case "LAYOUT_RessourceManager": resultat = "Gestor de Recursos"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Perfis Rápidos"; break;
                        case "LAYOUT_GBSystem": resultat = "Sistema"; break;
                        case "LAYOUT_GBAddOn": resultat = "Add-On"; break;

                        case "LAYOUT_ProfilSave": resultat = "Guardar Perfil"; break;
                        case "LAYOUT_File": resultat = "Ficheiro"; break;
                        case "LAYOUT_Profil": resultat = "Perfil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Atualizar Perfis"; break;
                        case "LAYOUT_ProfilReset": resultat = "Reiniciar Perfis"; break;
                        case "LAYOUT_Setting": resultat = "Definições"; break;
                        case "LAYOUT_About": resultat = "Sobre"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Contribuidores"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Arabic
                case "AR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "حدث خطأ :( يُرجى التأكد من أن هذا البرنامج موجود في مجلد 'mods' في مجلد .minecraft الخاص بك!\nتأكد أيضًا من تنزيل المودات!"; break;
                        case "ERR_TIMEOUT": resultat = "انتهت مدة الانتظار - تحقق من اتصالك."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "هذه الميزة لم تُنفذ بعد."; break;
                        case "ERR_EXEC-BATCH": resultat = "حدث خطأ أثناء تنفيذ ملف الدفع: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "يبدو أنه غير ممكن بدء تشغيل Minecraft."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "لم تحدد أي عنصر، تم تجاهل الإجراء."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "هذه الميزة لم تُنفذ بعد."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "هذه الميزة لم تُنفذ بعد. ستكون هذه الميزة متاحة في الإصدار "; break;

                        case "WARN_WRONGFOLDER": resultat = "مجلد خاطئ: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "يجب تشغيل البرنامج من مجلد 'mods' في مجلد .minecraft الخاص بك."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "انظر ماذا يحدث..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "فتح تقرير المشكلة."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "فتح رابط سجل التغيير، ملخص للتغييرات في كل تحديث."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "فتح مستودع GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "فتح الرابط لتنزيل Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "تنزيل / تحديث المودات"; break;

                        case "INFO_SUCCESS": resultat = "كل شيء تم بنجاح!"; break;
                        case "INFO_ABOUT": resultat = $"مودات البقاء الخاصة بي\nبواسطة MythMega\n2023\nالإصدار: {version}\nالبنية: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "أنا MythMega (لم أنشئ هذا القسم بعد، ليس مستعجلاً)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"المساهمون هم كما يلي:\n{getContributorsNamesAndLbl()}\nشكرًا لهم!"; break;

                        case "CONTRIBUTORS": resultat = "المساهمون"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "الترجمة"; break;
                        case "CONTRIB_DEV": resultat = "التطوير"; break;
                        case "CONTRIB_ARTIST": resultat = "الفنان / التصميم"; break;
                        case "CONTRIB_TESTER": resultat = "المُختبر"; break;
                        case "CONTRIB_OTHER": resultat = "آخر"; break;

                        case "LBL_WARNING": resultat = "تحذير!"; break;
                        case "LBL_CONFIRMATION": resultat = "هل أنت متأكد من أنك تريد تنفيذ هذا الإجراء؟"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "تحديد الكل"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "إلغاء تحديد الكل"; break;
                        case "LBL_STOP": resultat = "إيقاف"; break;
                        case "LBL_OUI": resultat = "نعم"; break;
                        case "LBL_NON": resultat = "لا"; break;
                        case "LBL_ANNULER": resultat = "إلغاء"; break;
                        case "LBL_LOCALISER": resultat = "تحديد موقع"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "مظاهر GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "مظاهر اللعبة"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "مظاهر الجلود"; break;
                        case "LAYOUT_Commande_Admin": resultat = "أوامر المشرف"; break;

                        case "LAYOUT_IsEnabled": resultat = "مُفعل"; break;

                        case "LAYOUT_EnableAll": resultat = "تفعيل الكل"; break;
                        case "LAYOUT_DisableAll": resultat = "إلغاء تفعيل الكل"; break;
                        case "LAYOUT_Execute": resultat = "تنفيذ"; break;
                        case "LAYOUT_InstallFabric": resultat = "تثبيت Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "ملاحظات التحديث"; break;
                        case "LAYOUT_GithubSource": resultat = "المصدر على GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "فتح مجلد المودات"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "تحديث / تنزيل المودات"; break;
                        case "LAYOUT_Update": resultat = "تحديث"; break;
                        case "LAYOUT_Report": resultat = "الإبلاغ عن مشكلة"; break;
                        case "LAYOUT_Quit": resultat = "الخروج"; break;
                        case "LAYOUT_StartMinecraft": resultat = "بدء Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "إدارة الشيدرز"; break;
                        case "LAYOUT_RessourceManager": resultat = "إدارة الموارد"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "ملفات تعريف سريعة"; break;
                        case "LAYOUT_GBSystem": resultat = "النظام"; break;
                        case "LAYOUT_GBAddOn": resultat = "الإضافة"; break;

                        case "LAYOUT_ProfilSave": resultat = "حفظ الملف الشخصي"; break;
                        case "LAYOUT_File": resultat = "ملف"; break;
                        case "LAYOUT_Profil": resultat = "ملف شخصي"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "تحديث الملفات الشخصية"; break;
                        case "LAYOUT_ProfilReset": resultat = "إعادة تعيين الملفات الشخصية"; break;
                        case "LAYOUT_Setting": resultat = "إعداد"; break;
                        case "LAYOUT_About": resultat = "حول"; break;
                        case "LAYOUT_Theme": resultat = "سمة"; break;
                        case "LAYOUT_Contributors": resultat = "المساهمون"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Russian
                case "RU":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Произошла ошибка :( Убедитесь, что это программное обеспечение размещено в папке 'mods' вашей директории .minecraft!\nТакже убедитесь, что вы скачали моды!"; break;
                        case "ERR_TIMEOUT": resultat = "Тайм-аут - Проверьте ваше соединение."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Эта функция еще не реализована."; break;
                        case "ERR_EXEC-BATCH": resultat = "Произошла ошибка во время выполнения пакетного файла: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Похоже, запуск Minecraft невозможен."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Вы не выбрали ни одного элемента, действие было проигнорировано."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Эта функция еще не реализована."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Эта функция еще не реализована. Эта функция будет доступна в версии "; break;

                        case "WARN_WRONGFOLDER": resultat = "Неправильная папка: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Программу необходимо запускать из папки 'mods' вашей директории .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Посмотреть, что это делает..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Открыть отчет об ошибке."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Открыть URL-адрес журнала изменений, сводка изменений для каждого обновления."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Открыть репозиторий GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Открыть URL-адрес для скачивания Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Загрузить / Обновить моды"; break;

                        case "INFO_SUCCESS": resultat = "Все прошло успешно!"; break;
                        case "INFO_ABOUT": resultat = $"Мои моды выживания\nот MythMega\n2023\nВерсия: {version}\nСборка: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Я MythMega (я еще не создал этот раздел, это менее срочно)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Следующие лица внесли свой вклад:\n{getContributorsNamesAndLbl()}\nБлагодарим их!"; break;

                        case "CONTRIBUTORS": resultat = "Участники"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Переводчик"; break;
                        case "CONTRIB_DEV": resultat = "Разработчик"; break;
                        case "CONTRIB_ARTIST": resultat = "Художник/Дизайнер"; break;
                        case "CONTRIB_TESTER": resultat = "Тестировщик"; break;
                        case "CONTRIB_OTHER": resultat = "Другое"; break;

                        case "LBL_WARNING": resultat = "Предупреждение!"; break;
                        case "LBL_CONFIRMATION": resultat = "Вы уверены, что хотите выполнить это действие?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Выделить все"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Снять выделение со всего"; break;
                        case "LBL_STOP": resultat = "Остановить"; break;
                        case "LBL_OUI": resultat = "Да"; break;
                        case "LBL_NON": resultat = "Нет"; break;
                        case "LBL_ANNULER": resultat = "Отмена"; break;
                        case "LBL_LOCALISER": resultat = "Определить местоположение"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Косметические GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Косметические игры"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Косметические скины"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Административные команды"; break;

                        case "LAYOUT_IsEnabled": resultat = "Включено"; break;

                        case "LAYOUT_EnableAll": resultat = "Включить все"; break;
                        case "LAYOUT_DisableAll": resultat = "Выключить все"; break;
                        case "LAYOUT_Execute": resultat = "Выполнить"; break;
                        case "LAYOUT_InstallFabric": resultat = "Установить Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Примечания к обновлению"; break;
                        case "LAYOUT_GithubSource": resultat = "Исходный код на GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Открыть папку модов"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Обновить / Загрузить моды"; break;
                        case "LAYOUT_Update": resultat = "Обновить"; break;
                        case "LAYOUT_Report": resultat = "Сообщить"; break;
                        case "LAYOUT_Quit": resultat = "Выйти"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Запустить Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Менеджер шейдеров"; break;
                        case "LAYOUT_RessourceManager": resultat = "Менеджер ресурсов"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Быстрые профили"; break;
                        case "LAYOUT_GBSystem": resultat = "Система"; break;
                        case "LAYOUT_GBAddOn": resultat = "Дополнение"; break;

                        case "LAYOUT_ProfilSave": resultat = "Сохранить профиль"; break;
                        case "LAYOUT_File": resultat = "Файл"; break;
                        case "LAYOUT_Profil": resultat = "Профиль"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Обновить профили"; break;
                        case "LAYOUT_ProfilReset": resultat = "Сбросить профили"; break;
                        case "LAYOUT_Setting": resultat = "Настройка"; break;
                        case "LAYOUT_About": resultat = "О программе"; break;
                        case "LAYOUT_Theme": resultat = "Тема"; break;
                        case "LAYOUT_Contributors": resultat = "Участники"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Tagalog
                case "TL":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "May naganap na error :( Mangyaring siguruhing nasa 'mods' na folder ng iyong .minecraft ang software na ito!\nSiguruhing na-download mo na rin ang mga mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Suriin ang iyong koneksyon."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Ang feature na ito ay hindi pa na-implement."; break;
                        case "ERR_EXEC-BATCH": resultat = "May naganap na error habang ina-eexecute ang batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sa tingin ko, hindi maaring simulan ang Minecraft."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Walang napili na item, ang aksyon ay hindi pinansin."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Ang feature na ito ay hindi pa na-implement."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Ang feature na ito ay hindi pa na-implement. Ang feature na ito ay magiging available sa bersyon "; break;

                        case "WARN_WRONGFOLDER": resultat = "Maling folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Ang software ay dapat i-execute mula sa 'mods' na folder ng iyong .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Tingnan kung ano ang nagaganap..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buksan ang ulat ng problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buksan ang URL ng changelog, buod ng mga pagbabago sa bawat update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buksan ang GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buksan ang URL para sa pag-download ng Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "I-download / I-update ang mga mods"; break;

                        case "INFO_SUCCESS": resultat = "Lahat ay nangyari nang matagumpay!"; break;
                        case "INFO_ABOUT": resultat = $"Aking mga Survival Mods\nni MythMega\n2023\nBersyon: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ako si MythMega (Hindi ko pa nilikha ang seksyon na ito, hindi ito gaanong urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Ang mga nag-contribute ay ang mga sumusunod:\n{getContributorsNamesAndLbl()}\nSalamat sa kanila!"; break;

                        case "CONTRIBUTORS": resultat = "Mga Nag-contribute"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Pagsasalin"; break;
                        case "CONTRIB_DEV": resultat = "Developer"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Disenyo"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Iba pa"; break;

                        case "LBL_WARNING": resultat = "Babala!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sigurado ka bang nais mong magawa ang hakbang na ito?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Piliin ang Lahat"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "I-deseselect ang Lahat"; break;
                        case "LBL_STOP": resultat = "Itigil"; break;
                        case "LBL_OUI": resultat = "Oo"; break;
                        case "LBL_NON": resultat = "Hindi"; break;
                        case "LBL_ANNULER": resultat = "Kanselahin"; break;
                        case "LBL_LOCALISER": resultat = "Hanapin"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Cosmetic GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Cosmetic Laro"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Cosmetic Skin"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Admin na mga Utos"; break;

                        case "LAYOUT_IsEnabled": resultat = "Pinagana"; break;

                        case "LAYOUT_EnableAll": resultat = "I-enable ang Lahat"; break;
                        case "LAYOUT_DisableAll": resultat = "I-disable ang Lahat"; break;
                        case "LAYOUT_Execute": resultat = "I-Execute"; break;
                        case "LAYOUT_InstallFabric": resultat = "I-install ang Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Mga Tala ng Update"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub Source Code"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Buksan ang Mod Folder"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "I-update / I-download ang Mga Mods"; break;
                        case "LAYOUT_Update": resultat = "I-update"; break;
                        case "LAYOUT_Report": resultat = "I-report"; break;
                        case "LAYOUT_Quit": resultat = "Mag-quit"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Simulan ang Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Shader Manager"; break;
                        case "LAYOUT_RessourceManager": resultat = "Ressource Manager"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Mabilis na Mga Profile"; break;
                        case "LAYOUT_GBSystem": resultat = "Systema"; break;
                        case "LAYOUT_GBAddOn": resultat = "Pamamahagi"; break;

                        case "LAYOUT_ProfilSave": resultat = "I-save ang Profile"; break;
                        case "LAYOUT_File": resultat = "File"; break;
                        case "LAYOUT_Profil": resultat = "Profile"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "I-refresh ang Mga Profile"; break;
                        case "LAYOUT_ProfilReset": resultat = "I-reset ang Mga Profile"; break;
                        case "LAYOUT_Setting": resultat = "Setting"; break;
                        case "LAYOUT_About": resultat = "Tungkol sa"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Mga Kontribyutor"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Hindi
                case "HI":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "कोई त्रुटि हुई :( कृपया सुनिश्चित करें कि यह सॉफ़्टवेयर आपके .minecraft के 'mods' फ़ोल्डर में रखा गया है!\nयह भी सुनिश्चित करें कि आपने मॉड डाउनलोड किए हैं!"; break;
                        case "ERR_TIMEOUT": resultat = "टाइमआउट - अपना कनेक्शन जांचें।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "यह सुविधा अब तक लागू नहीं की गई है।"; break;
                        case "ERR_EXEC-BATCH": resultat = "बैच फ़ाइल को निष्पादित करते समय एक त्रुटि हुई: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "लगता है कि Minecraft को लॉन्च करना संभव नहीं है।"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "आपने कोई आइटम चयन नहीं किया है, क्रिया नजरअंदाज की गई है।"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "यह सुविधा अब तक लागू नहीं की गई है।"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "यह सुविधा अब तक लागू नहीं की गई है। यह सुविधा संस्करण "; break;

                        case "WARN_WRONGFOLDER": resultat = "गलत फ़ोल्डर: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "सॉफ़्टवेयर को आपके .minecraft के 'mods' फ़ोल्डर से निष्पादित किया जाना चाहिए।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "देखें कि यह क्या करता है..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "समस्या रिपोर्ट खोलें।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "चेंजलॉग का URL खोलें, प्रत्येक अपडेट के लिए परिवर्तनों की संक्षेपित सूची।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub रिपॉजिटरी खोलें।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric डाउनलोड करने के लिए URL खोलें।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "मॉड्स डाउनलोड / अपडेट करें"; break;

                        case "INFO_SUCCESS": resultat = "सब कुछ सफलतापूर्वक हुआ!"; break;
                        case "INFO_ABOUT": resultat = $"मेरे Survival Mods\nद्वारा MythMega\n2023\nसंस्करण: {version}\nबिल्ड: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "मैं MythMega हूँ (मैंने अब तक इस खंड को नहीं बनाया है, यह कम अत्यंत आवश्यक है)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"यहाँ कुछ योगदानकर्ता हैं:\n{getContributorsNamesAndLbl()}\nउन्हें धन्यवाद!"; break;

                        case "CONTRIBUTORS": resultat = "योगदानकर्ता"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "अनुवादक"; break;
                        case "CONTRIB_DEV": resultat = "डेवलपर"; break;
                        case "CONTRIB_ARTIST": resultat = "कलाकार/डिजाइन"; break;
                        case "CONTRIB_TESTER": resultat = "टेस्टर"; break;
                        case "CONTRIB_OTHER": resultat = "अन्य"; break;

                        case "LBL_WARNING": resultat = "चेतावनी!"; break;
                        case "LBL_CONFIRMATION": resultat = "क्या आप इस क्रिया को करना चाहते हैं?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "सभी का चयन करें"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "सभी का चयन हटाएं"; break;
                        case "LBL_STOP": resultat = "रोकें"; break;
                        case "LBL_OUI": resultat = "हाँ"; break;
                        case "LBL_NON": resultat = "नहीं"; break;
                        case "LBL_ANNULER": resultat = "रद्द करें"; break;
                        case "LBL_LOCALISER": resultat = "स्थानांतरित करें"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "कॉस्मेटिक जीयूआई"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "कॉस्मेटिक गेम"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "कॉस्मेटिक स्किन"; break;
                        case "LAYOUT_Commande_Admin": resultat = "एडमिन कमांड"; break;

                        case "LAYOUT_IsEnabled": resultat = "सक्रिय"; break;

                        case "LAYOUT_EnableAll": resultat = "सभी को सक्रिय करें"; break;
                        case "LAYOUT_DisableAll": resultat = "सभी को निष्क्रिय करें"; break;
                        case "LAYOUT_Execute": resultat = "अमल करें"; break;
                        case "LAYOUT_InstallFabric": resultat = "फैब्रिक इंस्टॉल करें"; break;
                        case "LAYOUT_UpdateNotes": resultat = "अपडेट नोट्स"; break;
                        case "LAYOUT_GithubSource": resultat = "गिटहब स्रोत कोड"; break;
                        case "LAYOUT_OpenModFolder": resultat = "मॉड फ़ोल्डर खोलें"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "मॉड्स अपडेट / डाउनलोड करें"; break;
                        case "LAYOUT_Update": resultat = "अपडेट करें"; break;
                        case "LAYOUT_Report": resultat = "रिपोर्ट करें"; break;
                        case "LAYOUT_Quit": resultat = "बंद करें"; break;
                        case "LAYOUT_StartMinecraft": resultat = "माइनक्राफ़्ट शुरू करें"; break;
                        case "LAYOUT_ShaderManager": resultat = "शेडर प्रबंधक"; break;
                        case "LAYOUT_RessourceManager": resultat = "संसाधन प्रबंधक"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "त्वरित प्रोफाइल"; break;
                        case "LAYOUT_GBSystem": resultat = "सिस्टम"; break;
                        case "LAYOUT_GBAddOn": resultat = "अधिक"; break;

                        case "LAYOUT_ProfilSave": resultat = "प्रोफ़ाइल सहेजें"; break;
                        case "LAYOUT_File": resultat = "फ़ाइल"; break;
                        case "LAYOUT_Profil": resultat = "प्रोफ़ाइल"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "प्रोफ़ाइल रिफ़्रेश करें"; break;
                        case "LAYOUT_ProfilReset": resultat = "प्रोफ़ाइल रीसेट करें"; break;
                        case "LAYOUT_Setting": resultat = "सेटिंग"; break;
                        case "LAYOUT_About": resultat = "के बारे में"; break;
                        case "LAYOUT_Theme": resultat = "थीम"; break;
                        case "LAYOUT_Contributors": resultat = "कोन्ट्रिब्यूटर्स"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Bengali
                case "BN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "একটি ত্রুটি ঘটেছে :( দয়া করে নিশ্চিত করুন যে এই সফটওয়্যারটি আপনার .minecraft ফোল্ডারের 'mods' ফোল্ডারে রাখা হয়েছে!\nআপনি যদি মড ডাউনলোড করেননি তবে সম্ভবত আপনি করেননি!"; break;
                        case "ERR_TIMEOUT": resultat = "টাইমআউট - আপনার সংযোগ পরীক্ষা করুন।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "এই ফিচারটি এখনও লাগু করা হয়নি।"; break;
                        case "ERR_EXEC-BATCH": resultat = "ব্যাচ ফাইল নিষ্পাদন করার সময় একটি ত্রুটি ঘটেছে: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "মাইনক্রাফট চালানো সম্ভব হতে পারে না মনে হচ্ছে।"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "আপনি কোনও আইটেম নির্বাচন করননি, ক্রিয়াটি উপেক্ষা করা হয়েছে।"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "এই ফিচারটি এখনও লাগু করা হয়নি।"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "এই ফিচারটি এখনও লাগু করা হয়নি। এই ফিচারটি সংস্করণে "; break;

                        case "WARN_WRONGFOLDER": resultat = "ভুল ফোল্ডার: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "সফটওয়্যারটি আপনার .minecraft ফোল্ডারের 'mods' ফোল্ডার থেকে চালাতে হবে।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "দেখুন কি হচ্ছে..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "সমস্যা রিপোর্ট খুলুন।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "চেঞ্জলগের URL খুলুন, প্রতিটি আপডেটের সারসংক্ষেপ।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub রিপোজিটরি খুলুন।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "ফ্যাব্রিক ডাউনলোড করার URL খুলুন।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "মডগুলি আপডেট / ডাউনলোড করুন"; break;

                        case "INFO_SUCCESS": resultat = "সব সাফল্যে ঘটেছে!"; break;
                        case "INFO_ABOUT": resultat = $"আমার সার্ভাইভাল মডস\nদ্বারা MythMega\n2023\nসংস্করণ: {version}\nবিল্ড: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "আমি মিথমেগা (আমি এখনও এই সেকশনটি তৈরি করিনি, এটি কম জরুরি)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"যারা যোগদান করেছেন:\n{getContributorsNamesAndLbl()}\nতাদের জন্য ধন্যবাদ!"; break;

                        case "CONTRIBUTORS": resultat = "যোগদানকারী"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "অনুবাদক"; break;
                        case "CONTRIB_DEV": resultat = "ডেভেলপার"; break;
                        case "CONTRIB_ARTIST": resultat = "শিল্পী/ডিজাইন"; break;
                        case "CONTRIB_TESTER": resultat = "পরীক্ষাকারী"; break;
                        case "CONTRIB_OTHER": resultat = "অন্যান্য"; break;

                        case "LBL_WARNING": resultat = "সতর্কতা!"; break;
                        case "LBL_CONFIRMATION": resultat = "আপনি কি এই ক্রিয়া করতে চান?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "সব নির্বাচন করুন"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "সব নির্বাচন বাতিল করুন"; break;
                        case "LBL_STOP": resultat = "বন্ধ করুন"; break;
                        case "LBL_OUI": resultat = "হ্যাঁ"; break;
                        case "LBL_NON": resultat = "না"; break;
                        case "LBL_ANNULER": resultat = "বাতিল করুন"; break;
                        case "LBL_LOCALISER": resultat = "স্থানান্তর করুন"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "কসমেটিক জিউআই"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "কসমেটিক গেম"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "কসমেটিক ত্বক"; break;
                        case "LAYOUT_Commande_Admin": resultat = "অ্যাডমিন কমান্ড"; break;

                        case "LAYOUT_IsEnabled": resultat = "সক্রিয়"; break;

                        case "LAYOUT_EnableAll": resultat = "সব সক্রিয় করুন"; break;
                        case "LAYOUT_DisableAll": resultat = "সব নিষ্ক্রিয় করুন"; break;
                        case "LAYOUT_Execute": resultat = "প্রয়োগ করুন"; break;
                        case "LAYOUT_InstallFabric": resultat = "ফ্যাব্রিক ইনস্টল করুন"; break;
                        case "LAYOUT_UpdateNotes": resultat = "আপডেট নোটস"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub সোর্স কোড"; break;
                        case "LAYOUT_OpenModFolder": resultat = "মড ফোল্ডার খুলুন"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "মডগুলি আপডেট / ডাউনলোড করুন"; break;
                        case "LAYOUT_Update": resultat = "আপডেট করুন"; break;
                        case "LAYOUT_Report": resultat = "রিপোর্ট করুন"; break;
                        case "LAYOUT_Quit": resultat = "বন্ধ করুন"; break;
                        case "LAYOUT_StartMinecraft": resultat = "মাইনক্রাফট চালু করুন"; break;
                        case "LAYOUT_ShaderManager": resultat = "শেডার ম্যানেজার"; break;
                        case "LAYOUT_RessourceManager": resultat = "রিসোর্স ম্যানেজার"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "দ্রুত প্রোফাইল"; break;
                        case "LAYOUT_GBSystem": resultat = "সিস্টেম"; break;
                        case "LAYOUT_GBAddOn": resultat = "অ্যাডড-অন"; break;

                        case "LAYOUT_ProfilSave": resultat = "প্রোফাইল সংরক্ষণ করুন"; break;
                        case "LAYOUT_File": resultat = "ফাইল"; break;
                        case "LAYOUT_Profil": resultat = "প্রোফাইল"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "প্রোফাইল রিফ্রেশ করুন"; break;
                        case "LAYOUT_ProfilReset": resultat = "প্রোফাইল রিসেট করুন"; break;
                        case "LAYOUT_Setting": resultat = "সেটিং"; break;
                        case "LAYOUT_About": resultat = "সম্পর্কিত"; break;
                        case "LAYOUT_Theme": resultat = "থিম"; break;
                        case "LAYOUT_Contributors": resultat = "যোগদানকারী"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Japanese
                case "JA":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "エラーが発生しました :( このソフトウェアは .minecraft の 'mods' フォルダに配置されていることを確認してください！\nまた、モッドをダウンロードしていることを確認してください！"; break;
                        case "ERR_TIMEOUT": resultat = "タイムアウト - 接続を確認してください。"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "この機能はまだ実装されていません。"; break;
                        case "ERR_EXEC-BATCH": resultat = "バッチファイルの実行中にエラーが発生しました: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraftを起動することはできないようです。"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "アイテムが選択されていません。アクションは無視されました。"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "この機能はまだ実装されていません。"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "この機能はまだ実装されていません。この機能はバージョン "; break;

                        case "WARN_WRONGFOLDER": resultat = "間違ったフォルダ: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "このソフトウェアは .minecraft の 'mods' フォルダから実行する必要があります。"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "何が起こるか確認してみてください..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "問題の報告を開く。"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "変更ログのURLを開く。更新ごとの変更の概要。"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHubリポジトリを開く。"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "FabricのダウンロードURLを開く。"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "モッドのダウンロード / 更新"; break;

                        case "INFO_SUCCESS": resultat = "すべてが成功裏に行きました！"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\n作成者: MythMega\n2023\nバージョン: {version}\nビルド: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "私はMythMegaです（このセクションはまだ作成していません、緊急性は低いです）。"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"以下は貢献者の一覧です:\n{getContributorsNamesAndLbl()}\n彼らに感謝します！"; break;

                        case "CONTRIBUTORS": resultat = "貢献者"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "翻訳"; break;
                        case "CONTRIB_DEV": resultat = "開発"; break;
                        case "CONTRIB_ARTIST": resultat = "アーティスト/デザイン"; break;
                        case "CONTRIB_TESTER": resultat = "テスター"; break;
                        case "CONTRIB_OTHER": resultat = "その他"; break;

                        case "LBL_WARNING": resultat = "警告！"; break;
                        case "LBL_CONFIRMATION": resultat = "このアクションを実行してもよろしいですか？"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "すべて選択"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "すべて選択解除"; break;
                        case "LBL_STOP": resultat = "停止"; break;
                        case "LBL_OUI": resultat = "はい"; break;
                        case "LBL_NON": resultat = "いいえ"; break;
                        case "LBL_ANNULER": resultat = "キャンセル"; break;
                        case "LBL_LOCALISER": resultat = "検出"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "見た目のGUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "見た目のゲーム"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "見た目のスキン"; break;
                        case "LAYOUT_Commande_Admin": resultat = "管理者コマンド"; break;

                        case "LAYOUT_IsEnabled": resultat = "有効"; break;

                        case "LAYOUT_EnableAll": resultat = "すべて有効"; break;
                        case "LAYOUT_DisableAll": resultat = "すべて無効"; break;
                        case "LAYOUT_Execute": resultat = "実行"; break;
                        case "LAYOUT_InstallFabric": resultat = "Fabricのインストール"; break;
                        case "LAYOUT_UpdateNotes": resultat = "更新ノート"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHubソース"; break;
                        case "LAYOUT_OpenModFolder": resultat = "モッドフォルダを開く"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "モッドの更新/ダウンロード"; break;
                        case "LAYOUT_Update": resultat = "更新"; break;
                        case "LAYOUT_Report": resultat = "報告"; break;
                        case "LAYOUT_Quit": resultat = "終了"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Minecraftの起動"; break;
                        case "LAYOUT_ShaderManager": resultat = "シェーダーマネージャー"; break;
                        case "LAYOUT_RessourceManager": resultat = "リソースマネージャー"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "高速プロファイル"; break;
                        case "LAYOUT_GBSystem": resultat = "システム"; break;
                        case "LAYOUT_GBAddOn": resultat = "アドオン"; break;

                        case "LAYOUT_ProfilSave": resultat = "プロファイルの保存"; break;
                        case "LAYOUT_File": resultat = "ファイル"; break;
                        case "LAYOUT_Profil": resultat = "プロファイル"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "プロファイルの更新"; break;
                        case "LAYOUT_ProfilReset": resultat = "プロファイルのリセット"; break;
                        case "LAYOUT_Setting": resultat = "設定"; break;
                        case "LAYOUT_About": resultat = "情報"; break;
                        case "LAYOUT_Theme": resultat = "テーマ"; break;
                        case "LAYOUT_Contributors": resultat = "貢献者"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Korean
                case "KO":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "오류가 발생했습니다 :( 이 소프트웨어를 .minecraft의 'mods' 폴더에 배치해주세요!\n또한 모드를 다운로드했는지 확인하세요!"; break;
                        case "ERR_TIMEOUT": resultat = "타임아웃 - 연결을 확인하세요."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "이 기능은 아직 구현되지 않았습니다."; break;
                        case "ERR_EXEC-BATCH": resultat = "일괄 파일 실행 중 오류가 발생했습니다: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "마인크래프트를 실행할 수 없는 것 같습니다."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "아무 항목도 선택하지 않았습니다. 작업이 무시되었습니다."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "이 기능은 아직 구현되지 않았습니다."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "이 기능은 아직 구현되지 않았습니다. 이 기능은 버전에 포함될 예정입니다 "; break;

                        case "WARN_WRONGFOLDER": resultat = "잘못된 폴더: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "이 소프트웨어는 .minecraft의 'mods' 폴더에서 실행되어야 합니다."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "무엇을 하는지 확인해보세요..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "문제 보고서 열기."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "변경 로그 URL 열기, 업데이트별 변경 내용 요약."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub 저장소 열기."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric 다운로드 URL 열기."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "모드 다운로드 / 업데이트"; break;

                        case "INFO_SUCCESS": resultat = "모든 작업이 성공적으로 완료되었습니다!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\n제작자: MythMega\n2023\n버전: {version}\n빌드: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "저는 MythMega입니다 (이 섹션은 아직 만들지 않았습니다. 긴급도가 낮습니다)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"다음은 기여자 목록입니다:\n{getContributorsNamesAndLbl()}\n그들에게 감사드립니다!"; break;

                        case "CONTRIBUTORS": resultat = "기여자"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "번역"; break;
                        case "CONTRIB_DEV": resultat = "개발"; break;
                        case "CONTRIB_ARTIST": resultat = "아티스트/디자인"; break;
                        case "CONTRIB_TESTER": resultat = "테스터"; break;
                        case "CONTRIB_OTHER": resultat = "기타"; break;

                        case "LBL_WARNING": resultat = "경고!"; break;
                        case "LBL_CONFIRMATION": resultat = "이 작업을 수행하시겠습니까?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "모두 선택"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "모두 선택 해제"; break;
                        case "LBL_STOP": resultat = "중지"; break;
                        case "LBL_OUI": resultat = "예"; break;
                        case "LBL_NON": resultat = "아니오"; break;
                        case "LBL_ANNULER": resultat = "취소"; break;
                        case "LBL_LOCALISER": resultat = "찾기"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "화장품 GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "화장품 게임"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "화장품 스킨"; break;
                        case "LAYOUT_Commande_Admin": resultat = "관리자 명령어"; break;

                        case "LAYOUT_IsEnabled": resultat = "활성화됨"; break;

                        case "LAYOUT_EnableAll": resultat = "모두 활성화"; break;
                        case "LAYOUT_DisableAll": resultat = "모두 비활성화"; break;
                        case "LAYOUT_Execute": resultat = "실행"; break;
                        case "LAYOUT_InstallFabric": resultat = "Fabric 설치"; break;
                        case "LAYOUT_UpdateNotes": resultat = "업데이트 노트"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub 소스"; break;
                        case "LAYOUT_OpenModFolder": resultat = "모드 폴더 열기"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "모드 업데이트 / 다운로드"; break;
                        case "LAYOUT_Update": resultat = "업데이트"; break;
                        case "LAYOUT_Report": resultat = "보고"; break;
                        case "LAYOUT_Quit": resultat = "종료"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Minecraft 시작"; break;
                        case "LAYOUT_ShaderManager": resultat = "쉐이더 관리자"; break;
                        case "LAYOUT_RessourceManager": resultat = "자원 관리자"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "빠른 프로필"; break;
                        case "LAYOUT_GBSystem": resultat = "시스템"; break;
                        case "LAYOUT_GBAddOn": resultat = "추가 기능"; break;

                        case "LAYOUT_ProfilSave": resultat = "프로필 저장"; break;
                        case "LAYOUT_File": resultat = "파일"; break;
                        case "LAYOUT_Profil": resultat = "프로필"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "프로필 새로 고침"; break;
                        case "LAYOUT_ProfilReset": resultat = "프로필 재설정"; break;
                        case "LAYOUT_Setting": resultat = "설정"; break;
                        case "LAYOUT_About": resultat = "소개"; break;
                        case "LAYOUT_Theme": resultat = "테마"; break;
                        case "LAYOUT_Contributors": resultat = "기여자"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Turkish
                case "TR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Bir hata oluştu :( Lütfen bu yazılımı .minecraft klasörünün 'mods' klasörüne yerleştirin!\nAyrıca modları indirdiğinizden emin olun!"; break;
                        case "ERR_TIMEOUT": resultat = "Zaman aşımı - Bağlantınızı kontrol edin."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Bu özellik henüz uygulanmadı."; break;
                        case "ERR_EXEC-BATCH": resultat = "Toplu iş dosyası çalıştırılırken bir hata oluştu: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft'ı başlatmak mümkün gibi görünmüyor."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Hiçbir öğe seçmediniz, işlem yok sayıldı."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Bu özellik henüz uygulanmadı."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Bu özellik henüz uygulanmadı. Bu özellik sürümünde mevcut olacak "; break;

                        case "WARN_WRONGFOLDER": resultat = "Yanlış klasör: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Yazılımı .minecraft'ın 'mods' klasöründen başlatmanız gerekmektedir."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Ne yaptığını görmek..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Sorun raporunu aç."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Değişiklik günlüğü URL'sini aç, güncellemelerle ilgili değişikliklerin özeti."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub deposunu aç."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric indirme URL'sini aç."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Modları İndir / Güncelle"; break;

                        case "INFO_SUCCESS": resultat = "Her şey başarılı bir şekilde geçti!"; break;
                        case "INFO_ABOUT": resultat = $"Benim Hayatta Kalma Modlarım\nYazar: MythMega\n2023\nSürüm: {version}\nDerleme: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ben MythMega'yım (Bu bölümü henüz oluşturmadım, aciliyeti düşüktür)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Katılımcılar aşağıdaki gibidir:\n{getContributorsNamesAndLbl()}\nOnlara teşekkür ederiz!"; break;

                        case "CONTRIBUTORS": resultat = "Katılımcılar"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Çeviri"; break;
                        case "CONTRIB_DEV": resultat = "Geliştirme"; break;
                        case "CONTRIB_ARTIST": resultat = "Sanatçı/Tasarım"; break;
                        case "CONTRIB_TESTER": resultat = "Test Eden"; break;
                        case "CONTRIB_OTHER": resultat = "Diğer"; break;

                        case "LBL_WARNING": resultat = "Uyarı!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bu işlemi gerçekleştirmek istediğinizden emin misiniz?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tümünü Seç"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tümünü Seçme"; break;
                        case "LBL_STOP": resultat = "Durdur"; break;
                        case "LBL_OUI": resultat = "Evet"; break;
                        case "LBL_NON": resultat = "Hayır"; break;
                        case "LBL_ANNULER": resultat = "İptal"; break;
                        case "LBL_LOCALISER": resultat = "Yerini Bul"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Kozmetik Arayüzü"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Kozmetik Oyun"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Kozmetik Kılıçlar"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Yönetici Komutları"; break;

                        case "LAYOUT_IsEnabled": resultat = "Etkin"; break;

                        case "LAYOUT_EnableAll": resultat = "Tümünü Etkinleştir"; break;
                        case "LAYOUT_DisableAll": resultat = "Tümünü Devre Dışı Bırak"; break;
                        case "LAYOUT_Execute": resultat = "Çalıştır"; break;
                        case "LAYOUT_InstallFabric": resultat = "Fabric'i Kur"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Güncelleme Notları"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub Kaynağı"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Mod Klasörünü Aç"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Modları Güncelle / İndir"; break;
                        case "LAYOUT_Update": resultat = "Güncelle"; break;
                        case "LAYOUT_Report": resultat = "Rapor"; break;
                        case "LAYOUT_Quit": resultat = "Çık"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Minecraft'i Başlat"; break;
                        case "LAYOUT_ShaderManager": resultat = "Shader Yöneticisi"; break;
                        case "LAYOUT_RessourceManager": resultat = "Kaynak Yöneticisi"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Hızlı Profiller"; break;
                        case "LAYOUT_GBSystem": resultat = "Sistem"; break;
                        case "LAYOUT_GBAddOn": resultat = "Ek Özellik"; break;

                        case "LAYOUT_ProfilSave": resultat = "Profili Kaydet"; break;
                        case "LAYOUT_File": resultat = "Dosya"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Profilleri Yenile"; break;
                        case "LAYOUT_ProfilReset": resultat = "Profilleri Sıfırla"; break;
                        case "LAYOUT_Setting": resultat = "Ayarlar"; break;
                        case "LAYOUT_About": resultat = "Hakkında"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Katılımcılar"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Thai
                case "TH":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "เกิดข้อผิดพลาด :( โปรดตรวจสอบให้แน่ใจว่าซอฟต์แวร์นี้ถูกวางไว้ในโฟลเดอร์ 'mods' ของ .minecraft!\nและตรวจสอบให้แน่ใจว่าคุณได้ดาวน์โหลดม็อด!"; break;
                        case "ERR_TIMEOUT": resultat = "หมดเวลา - ตรวจสอบการเชื่อมต่อของคุณ"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "คุณลักษณะนี้ยังไม่ได้รับการนำมาใช้"; break;
                        case "ERR_EXEC-BATCH": resultat = "เกิดข้อผิดพลาดขณะที่กำลังดำเนินการแบบชุด: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "ดูเหมือนว่าจะไม่สามารถเริ่ม Minecraft ได้"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "คุณไม่ได้เลือกรายการใด การกระทำถูกละเลย"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "คุณลักษณะนี้ยังไม่ได้รับการนำมาใช้"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "คุณลักษณะนี้ยังไม่ได้รับการนำมาใช้ คุณลักษณะนี้จะมีในรุ่น "; break;

                        case "WARN_WRONGFOLDER": resultat = "โฟลเดอร์ผิด: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "ซอฟต์แวร์ต้องถูกเรียกใช้จากโฟลเดอร์ 'mods' ของ .minecraft"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "ดูสิ่งที่มันทำ..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "เปิดรายงานปัญหา"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "เปิด URL สรุปของบันทึกการเปลี่ยนแปลง, สรุปของการเปลี่ยนแปลงต่อการอัปเดต"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "เปิดรักเกียรติ GitHub"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "เปิด URL ในการดาวน์โหลด Fabric"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "ดาวน์โหลด / อัปเดตม็อด"; break;

                        case "INFO_SUCCESS": resultat = "ทุกอย่างเสร็จสมบูรณ์แล้ว!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nโดย MythMega\nปี 2023\nเวอร์ชัน: {version}\nรุ่น: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "ฉันคือ MythMega (ฉันยังไม่ได้สร้างส่วนนี้ ไม่สำคัญ)"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"ผู้มีส่วนร่วมคือดังนี้:\n{getContributorsNamesAndLbl()}\nขอบคุณพวกเขา!"; break;

                        case "CONTRIBUTORS": resultat = "ผู้มีส่วนร่วม"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "การแปล"; break;
                        case "CONTRIB_DEV": resultat = "การพัฒนา"; break;
                        case "CONTRIB_ARTIST": resultat = "ศิลปิน/การออกแบบ"; break;
                        case "CONTRIB_TESTER": resultat = "ผู้ทดสอบ"; break;
                        case "CONTRIB_OTHER": resultat = "อื่น ๆ"; break;

                        case "LBL_WARNING": resultat = "คำเตือน!"; break;
                        case "LBL_CONFIRMATION": resultat = "คุณแน่ใจหรือไม่ว่าต้องการดำเนินการนี้?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "เลือกทั้งหมด"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "ยกเลิกการเลือกทั้งหมด"; break;
                        case "LBL_STOP": resultat = "หยุด"; break;
                        case "LBL_OUI": resultat = "ใช่"; break;
                        case "LBL_NON": resultat = "ไม่"; break;
                        case "LBL_ANNULER": resultat = "ยกเลิก"; break;
                        case "LBL_LOCALISER": resultat = "ค้นหาตำแหน่ง"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "UI ที่เปล่งปลั่ง"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "เกมที่เปล่งปลั่ง"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "สกินที่เปล่งปลั่ง"; break;
                        case "LAYOUT_Commande_Admin": resultat = "คำสั่งผู้ดูแล"; break;

                        case "LAYOUT_IsEnabled": resultat = "เปิดใช้งานแล้ว"; break;

                        case "LAYOUT_EnableAll": resultat = "เปิดใช้งานทั้งหมด"; break;
                        case "LAYOUT_DisableAll": resultat = "ปิดใช้งานทั้งหมด"; break;
                        case "LAYOUT_Execute": resultat = "ดำเนินการ"; break;
                        case "LAYOUT_InstallFabric": resultat = "ติดตั้ง Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "บันทึกการอัปเดต"; break;
                        case "LAYOUT_GithubSource": resultat = "แหล่งข้อมูล GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "เปิดโฟลเดอร์ม็อด"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "อัปเดต/ดาวน์โหลดม็อด"; break;
                        case "LAYOUT_Update": resultat = "อัปเดต"; break;
                        case "LAYOUT_Report": resultat = "รายงาน"; break;
                        case "LAYOUT_Quit": resultat = "ออก"; break;
                        case "LAYOUT_StartMinecraft": resultat = "เริ่ม Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "ผู้จัดการชาเดอร์"; break;
                        case "LAYOUT_RessourceManager": resultat = "ผู้จัดการทรัพยากร"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "โปรไฟล์ด่วน"; break;
                        case "LAYOUT_GBSystem": resultat = "ระบบ"; break;
                        case "LAYOUT_GBAddOn": resultat = "ประสิทธิภาพเพิ่มเติม"; break;

                        case "LAYOUT_ProfilSave": resultat = "บันทึกโปรไฟล์"; break;
                        case "LAYOUT_File": resultat = "ไฟล์"; break;
                        case "LAYOUT_Profil": resultat = "โปรไฟล์"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "รีเฟรชโปรไฟล์"; break;
                        case "LAYOUT_ProfilReset": resultat = "รีเซ็ตโปรไฟล์"; break;
                        case "LAYOUT_Setting": resultat = "ตั้งค่า"; break;
                        case "LAYOUT_About": resultat = "เกี่ยวกับ"; break;
                        case "LAYOUT_Theme": resultat = "ธีม"; break;
                        case "LAYOUT_Contributors": resultat = "ผู้มีส่วนร่วม"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Indonesian
                case "ID":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Terjadi kesalahan :( Pastikan perangkat lunak ini ditempatkan di dalam folder 'mods' dari .minecraft Anda!\nPastikan Anda juga telah mengunduh mod-modnya!"; break;
                        case "ERR_TIMEOUT": resultat = "Waktu habis - Periksa koneksi Anda."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Fitur ini belum diimplementasikan."; break;
                        case "ERR_EXEC-BATCH": resultat = "Terjadi kesalahan saat menjalankan file batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Tampaknya peluncuran Minecraft tidak mungkin dilakukan."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Anda belum memilih item apa pun, tindakan diabaikan."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Fitur ini belum diimplementasikan."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Fitur ini belum diimplementasikan. Fitur ini akan tersedia pada versi "; break;

                        case "WARN_WRONGFOLDER": resultat = "Folder salah: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Perangkat lunak harus dijalankan dari folder 'mods' dari .minecraft Anda."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Lihat apa yang terjadi..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buka laporan masalah."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buka URL catatan perubahan, ringkasan perubahan per pembaruan."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buka repositori GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buka URL untuk mengunduh Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Unduh / Perbarui mod"; break;

                        case "INFO_SUCCESS": resultat = "Semuanya berjalan dengan sukses!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\noleh MythMega\n2023\nVersi: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Saya MythMega (Saya belum membuat bagian ini, itu kurang mendesak)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Para kontributor adalah sebagai berikut:\n{getContributorsNamesAndLbl()}\nTerima kasih kepada mereka!"; break;

                        case "CONTRIBUTORS": resultat = "Kontributor"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Terjemahan"; break;
                        case "CONTRIB_DEV": resultat = "Pengembangan"; break;
                        case "CONTRIB_ARTIST": resultat = "Seniman/Desain"; break;
                        case "CONTRIB_TESTER": resultat = "Penguji"; break;
                        case "CONTRIB_OTHER": resultat = "Lainnya"; break;

                        case "LBL_WARNING": resultat = "Peringatan!"; break;
                        case "LBL_CONFIRMATION": resultat = "Anda yakin ingin melakukan tindakan ini?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Pilih Semua"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Batalkan Pilihan Semua"; break;
                        case "LBL_STOP": resultat = "Berhenti"; break;
                        case "LBL_OUI": resultat = "Ya"; break;
                        case "LBL_NON": resultat = "Tidak"; break;
                        case "LBL_ANNULER": resultat = "Batalkan"; break;
                        case "LBL_LOCALISER": resultat = "Lokalisasi"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Kosmetik GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Kosmetik Permainan"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Kosmetik Kulit"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Perintah Admin"; break;

                        case "LAYOUT_IsEnabled": resultat = "Diaktifkan"; break;

                        case "LAYOUT_EnableAll": resultat = "Aktifkan Semua"; break;
                        case "LAYOUT_DisableAll": resultat = "Nonaktifkan Semua"; break;
                        case "LAYOUT_Execute": resultat = "Jalankan"; break;
                        case "LAYOUT_InstallFabric": resultat = "Pasang Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Catatan Pembaruan"; break;
                        case "LAYOUT_GithubSource": resultat = "Sumber GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Buka Folder Mod"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Perbarui/Unduh Mod"; break;
                        case "LAYOUT_Update": resultat = "Perbarui"; break;
                        case "LAYOUT_Report": resultat = "Laporkan"; break;
                        case "LAYOUT_Quit": resultat = "Keluar"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Mulai Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Pengelola Shader"; break;
                        case "LAYOUT_RessourceManager": resultat = "Pengelola Sumber Daya"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Profil Cepat"; break;
                        case "LAYOUT_GBSystem": resultat = "Sistem"; break;
                        case "LAYOUT_GBAddOn": resultat = "Tambahkan-On"; break;

                        case "LAYOUT_ProfilSave": resultat = "Simpan Profil"; break;
                        case "LAYOUT_File": resultat = "Berkas"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Segarkan Profil"; break;
                        case "LAYOUT_ProfilReset": resultat = "Reset Profil"; break;
                        case "LAYOUT_Setting": resultat = "Pengaturan"; break;
                        case "LAYOUT_About": resultat = "Tentang"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Kontributor"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Polak
                case "PL":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Wystąpił błąd :( Upewnij się, że to oprogramowanie znajduje się w folderze 'mods' twojego .minecraft!\nUpewnij się również, że pobrałeś mody!"; break;
                        case "ERR_TIMEOUT": resultat = "Przekroczono limit czasu - Sprawdź swoje połączenie."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Ta funkcja nie została jeszcze zaimplementowana."; break;
                        case "ERR_EXEC-BATCH": resultat = "Wystąpił błąd podczas wykonywania pliku wsadowego: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Wygląda na to, że uruchomienie Minecrafta jest niemożliwe."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Nie wybrałeś żadnych przedmiotów, działanie zostało zignorowane."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Ta funkcja nie została jeszcze zaimplementowana."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Ta funkcja nie została jeszcze zaimplementowana. Ta funkcja będzie dostępna w wersji "; break;

                        case "WARN_WRONGFOLDER": resultat = "Zły folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Oprogramowanie musi być uruchomione z folderu 'mods' twojego .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Zobacz, co to robi..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Otwórz raport problemu."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Otwórz adres URL dziennika zmian, podsumowanie zmian w aktualizacjach."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Otwórz repozytorium GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Otwórz adres URL do pobrania Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Pobierz / Zaktualizuj mody"; break;

                        case "INFO_SUCCESS": resultat = "Wszystko przebiegło pomyślnie!"; break;
                        case "INFO_ABOUT": resultat = $"Moje Moduły Przetrwania\nautorstwa MythMega\n2023\nWersja: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Jestem MythMega (jeszcze nie stworzyłem tej sekcji, jest to mniej pilne)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Współtwórcy to następujący:\n{getContributorsNamesAndLbl()}\nDzięki nim!"; break;

                        case "CONTRIBUTORS": resultat = "Współtwórcy"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Tłumaczenie"; break;
                        case "CONTRIB_DEV": resultat = "Rozwój"; break;
                        case "CONTRIB_ARTIST": resultat = "Artysta/Projektant"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Inne"; break;

                        case "LBL_WARNING": resultat = "Ostrzeżenie!"; break;
                        case "LBL_CONFIRMATION": resultat = "Czy na pewno chcesz wykonać tę operację?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Zaznacz wszystko"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Odznacz wszystko"; break;
                        case "LBL_STOP": resultat = "Zatrzymaj"; break;
                        case "LBL_OUI": resultat = "Tak"; break;
                        case "LBL_NON": resultat = "Nie"; break;
                        case "LBL_ANNULER": resultat = "Anuluj"; break;
                        case "LBL_LOCALISER": resultat = "Zlokalizuj"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Kosmetyczny interfejs GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Kosmetyka gry"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Kosmetyczne skórki"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Polecenia administratora"; break;

                        case "LAYOUT_IsEnabled": resultat = "Włączone"; break;

                        case "LAYOUT_EnableAll": resultat = "Włącz wszystko"; break;
                        case "LAYOUT_DisableAll": resultat = "Wyłącz wszystko"; break;
                        case "LAYOUT_Execute": resultat = "Wykonaj"; break;
                        case "LAYOUT_InstallFabric": resultat = "Zainstaluj Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Notatki aktualizacji"; break;
                        case "LAYOUT_GithubSource": resultat = "Źródło GitHub"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Otwórz folder modów"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Aktualizuj/Pobierz mody"; break;
                        case "LAYOUT_Update": resultat = "Aktualizuj"; break;
                        case "LAYOUT_Report": resultat = "Zgłoś"; break;
                        case "LAYOUT_Quit": resultat = "Zakończ"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Uruchom Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Menadżer shaderów"; break;
                        case "LAYOUT_RessourceManager": resultat = "Menadżer zasobów"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Szybkie profile"; break;
                        case "LAYOUT_GBSystem": resultat = "System"; break;
                        case "LAYOUT_GBAddOn": resultat = "Dodatki"; break;

                        case "LAYOUT_ProfilSave": resultat = "Zapisz profil"; break;
                        case "LAYOUT_File": resultat = "Plik"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Odśwież profile"; break;
                        case "LAYOUT_ProfilReset": resultat = "Resetuj profile"; break;
                        case "LAYOUT_Setting": resultat = "Ustawienia"; break;
                        case "LAYOUT_About": resultat = "O programie"; break;
                        case "LAYOUT_Theme": resultat = "Motyw"; break;
                        case "LAYOUT_Contributors": resultat = "Współtwórcy"; break;

                        case "": resultat = ""; break;

                    }
                    break;

                // Polak
                case "SV":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ett fel uppstod :( Se till att den här programvaran är placerad i 'mods'-mappen i din .minecraft!\nSäkerställ också att du har laddat ner modsen!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Kontrollera din anslutning."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Denna funktion är ännu inte implementerad."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ett fel uppstod vid utförandet av batch-filen: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Det verkar som att starta Minecraft inte är möjligt."; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Du har inte valt några objekt, åtgärden ignorerades."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Denna funktion har ännu inte implementerats."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Denna funktion har ännu inte implementerats. Denna funktion kommer att finnas tillgänglig i version "; break;

                        case "WARN_WRONGFOLDER": resultat = "Fel mapp: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Programvaran måste köras från 'mods'-mappen i din .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Se vad det gör..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Öppna problemrapporten."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Öppna changelog-URL:en, sammanfattning av ändringar per uppdatering."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Öppna GitHub-repot."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Öppna URL:en för att ladda ner Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Ladda ner / Uppdatera mods"; break;

                        case "INFO_SUCCESS": resultat = "Allt gick bra!"; break;
                        case "INFO_ABOUT": resultat = $"Mina Överlevnadsmods\nav MythMega\n2023\nVersion: {version}\nBygg: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Jag är MythMega (jag har ännu inte skapat den här sektionen, det är mindre bråttom)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Bidragsgivarna är följande:\n{getContributorsNamesAndLbl()}\nTack till dem!"; break;

                        case "CONTRIBUTORS": resultat = "Bidragsgivare"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Översättning"; break;
                        case "CONTRIB_DEV": resultat = "Utveckling"; break;
                        case "CONTRIB_ARTIST": resultat = "Konstnär/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testare"; break;
                        case "CONTRIB_OTHER": resultat = "Annat"; break;

                        case "LBL_WARNING": resultat = "Varning!"; break;
                        case "LBL_CONFIRMATION": resultat = "Är du säker på att du vill utföra denna åtgärd?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Välj alla"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Avmarkera alla"; break;
                        case "LBL_STOP": resultat = "Stoppa"; break;
                        case "LBL_OUI": resultat = "Ja"; break;
                        case "LBL_NON": resultat = "Nej"; break;
                        case "LBL_ANNULER": resultat = "Avbryt"; break;
                        case "LBL_LOCALISER": resultat = "Lokalisera"; break;

                        case "LAYOUT_Cosmetic_GUI": resultat = "Kosmetiskt GUI"; break;
                        case "LAYOUT_Cosmetic_Game": resultat = "Kosmetiskt Spel"; break;
                        case "LAYOUT_Cosmetic_Skin": resultat = "Kosmetiska Skins"; break;
                        case "LAYOUT_Commande_Admin": resultat = "Admin Kommandon"; break;

                        case "LAYOUT_IsEnabled": resultat = "Är aktiverad"; break;

                        case "LAYOUT_EnableAll": resultat = "Aktivera alla"; break;
                        case "LAYOUT_DisableAll": resultat = "Inaktivera alla"; break;
                        case "LAYOUT_Execute": resultat = "Utför"; break;
                        case "LAYOUT_InstallFabric": resultat = "Installera Fabric"; break;
                        case "LAYOUT_UpdateNotes": resultat = "Uppdateringsanteckningar"; break;
                        case "LAYOUT_GithubSource": resultat = "GitHub-källa"; break;
                        case "LAYOUT_OpenModFolder": resultat = "Öppna Mod-mappen"; break;
                        case "LAYOUT_UpdateDownloadMod": resultat = "Uppdatera/Ladda ner mods"; break;
                        case "LAYOUT_Update": resultat = "Uppdatera"; break;
                        case "LAYOUT_Report": resultat = "Rapportera"; break;
                        case "LAYOUT_Quit": resultat = "Avsluta"; break;
                        case "LAYOUT_StartMinecraft": resultat = "Starta Minecraft"; break;
                        case "LAYOUT_ShaderManager": resultat = "Shaderhanterare"; break;
                        case "LAYOUT_RessourceManager": resultat = "Resurshanterare"; break;

                        case "LAYOUT_GBFastProfiles": resultat = "Snabba profiler"; break;
                        case "LAYOUT_GBSystem": resultat = "System"; break;
                        case "LAYOUT_GBAddOn": resultat = "Tillägg"; break;

                        case "LAYOUT_ProfilSave": resultat = "Spara Profil"; break;
                        case "LAYOUT_File": resultat = "Fil"; break;
                        case "LAYOUT_Profil": resultat = "Profil"; break;
                        case "LAYOUT_ProfilRefresh": resultat = "Uppdatera Profiler"; break;
                        case "LAYOUT_ProfilReset": resultat = "Återställ Profiler"; break;
                        case "LAYOUT_Setting": resultat = "Inställningar"; break;
                        case "LAYOUT_About": resultat = "Om"; break;
                        case "LAYOUT_Theme": resultat = "Tema"; break;
                        case "LAYOUT_Contributors": resultat = "Bidragsgivare"; break;

                        case "": resultat = ""; break;
                    }
                    break;
            }
            return resultat;
        }
    }
}
