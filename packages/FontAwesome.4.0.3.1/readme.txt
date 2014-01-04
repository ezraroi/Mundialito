***New File Structure!***

Note: 
Some may have experienced a web optimization issue with the previous version if bundling css together due to relative paths in the css. 
This change should also resolve that issue. Just update your BundleConfig with the new css path.

More info on why this change occured can be found below:

The folks at Outercurve have taken over support for the Twitter.Bootstrap package now being called simply bootstrap. 
More info here http://chriskirby.net/bootstrap-nuget-package-moving-to-outercurve/

As part of this change they have reworked the file structure placing the fonts folder into application root and the css files into the Content root.
So in an effort to keep the asset locations as clean as possible the FontAwesome package will be following suit and moving this way too.

Feel free to report any concerns at https://github.com/JustLikeIcarus/Font-Awesome-NuGet/issues


