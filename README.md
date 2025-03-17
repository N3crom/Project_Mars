# Fonctionnalités du Jeu

## Fog of War
Le fog of war génère des tuiles grises sur une grille de 60x60. Les tuiles de fog adjacentes au joueur sont supprimées lors du spawn et à chaque mouvement.

## Stamina
La stamina correspond au nombre maximum de déplacements que peut faire le joueur dans un niveau. Une stamina de 0 fait perdre la partie. Si le joueur arrive sur la case de sortie avec une stamina de 0, il réussit le niveau. La stamina peut être modifiée pour chaque niveau dans le component `PlayerController` du joueur.

## Ghost Effect
Un pick-up peut être ajouté aux tuiles depuis les SSO "LevelGridData". Lorsque le joueur marche sur un `GhostPickUp`, il peut marcher à travers les murs pendant 2 secondes. Si le timer arrive à zéro alors que le joueur est dans un mur, il perd la partie.

**TODO:** Ajouter une indication de temps restant.

## Scene Management
- Un niveau = une scène.
- Le niveau suivant est à définir dans le game object `SceneLoader` dans le paramètre `NextLevelName`. La string correspond au nom de la scène à charger en cas de réussite du niveau.
