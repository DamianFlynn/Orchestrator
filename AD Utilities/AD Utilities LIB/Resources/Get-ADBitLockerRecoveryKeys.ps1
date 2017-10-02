Import-Module ActiveDirectory

function Get-BitLockerRecoveryKey {
	[cmdletbinding()]
	param(
		[Parameter(Mandatory = $true)] 
        [string]$Computer=$(Throw “Parameter ‘Mailbox’ cannot be empty”)            
	)  
   
    begin {    
		# Prepair an Array for the report
        $obj = New-Object PSObject
        $obj | Add-Member NoteProperty CanonicalName ("")
        $obj | Add-Member NoteProperty RecoveryPassword ("")
	}
   
    process {
		$compObj = Get-ADComputer $Computer
        $bitlockerInfo = get-ADObject -ldapfilter "(msFVE-Recoverypassword=*)" -Searchbase $compObj.distinguishedname -properties canonicalname,msfve-recoverypassword

        ##Loop through as their may be multiple saved
        foreach ($recoveryEntry in $bitlockerInfo)
        {
			$obj.CanonicalName = $RecoveryEntry.canonicalname
            $obj.RecoveryPassword = $RecoveryEntry."msfve-recoverypassword"
            write-output $obj
		}
	}
}
